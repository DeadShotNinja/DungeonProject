using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DP.Runtime
{
    [Serializable]
    class DungeonRoom_Portal
    {

    }

    public class DungeonRoom : MonoBehaviour
    {
        Vector2Int Size;
        Vector2 Center;

        Vector2 softPosition;
        Vector2Int hardPosition;

        List<DungeonRoom_Portal> Portals = new List<DungeonRoom_Portal>();

        public void Setup(Vector2Int roomSize)
        {
            softPosition = Vector3To2(transform.position);
            hardPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);

            Size = roomSize;

            var offset = new Vector2(Size.x / 2, Size.y / 2);
            Center = offset;
        }

        public bool isMoveOnCollide(DungeonRoom otherRoom)
        {
            if (isCollided(otherRoom))
            {
                var pos1 = hardPosition + Center;
                var pos2 = otherRoom.hardPosition + otherRoom.Center;

                var pos = pos2 - pos1;
                pos.Normalize();

                softPosition -= pos;

                hardPosition.x = Mathf.RoundToInt(softPosition.x);
                hardPosition.y = Mathf.RoundToInt(softPosition.y);


                //transform.position = Vector2To3(hardPosition);

                return true;
            }

            return false;
        }

        bool isCollided(DungeonRoom otherRoom)
        {
            Rect rA = new Rect();
            rA.center = new Vector2(hardPosition.x, hardPosition.y);
            rA.width = Size.x;
            rA.height = Size.y;

            Rect rB = new Rect();
            rB.center = new Vector2(otherRoom.hardPosition.x, otherRoom.hardPosition.y);
            rB.width = otherRoom.Size.x;
            rB.height = otherRoom.Size.y;

            bool hit = RectOverlapsRect(rA, rB);
            return hit;
        }

        bool RectOverlapsRect(Rect rA, Rect rB)
        {
            return (rA.x < rB.x + rB.width && rA.x + rA.width > rB.x && rA.y < rB.y + rB.height && rA.y + rA.height > rB.y);
        }

        private void OnDrawGizmos()
        {
            Rect rA = new Rect();
            rA.center = new Vector2(transform.position.x, transform.position.z);
            rA.width = Size.x;
            rA.height = Size.y;

            DrawRect(rA);

            void DrawRect(Rect rect)
            {
                Gizmos.DrawWireCube(new Vector3(rect.center.x, 0.01f, rect.center.y), new Vector3(rect.size.x, 0.01f, rect.size.y));
            }
        }

        public void UpdatePosition()
        {
            transform.position = Vector2To3(hardPosition);
        }

        Vector2 Vector3To2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
        Vector3 Vector2To3(Vector2 vector)
        {
            return new Vector3(vector.x, 0.0f, vector.y);
        }

        public void CalculateAllSideSegments()
        {
            for (int x = 0; x < Size.x; x++)
            {

            }
        }

        void CalculateSideSegments()
        {

        }
        public BoundsInt CalculateBounds()
        {
            BoundsInt bounds = new BoundsInt();
            bounds.xMin = (int)hardPosition.x;
            bounds.yMin = (int)hardPosition.y;
            bounds.xMax = (int)hardPosition.x + Size.x;
            bounds.yMax = (int)hardPosition.y + Size.y;

            return bounds;
        }

        public void UpdateBoundedPosition(BoundsInt bounds)
        {
            hardPosition.x -= bounds.xMin;
            hardPosition.y -= bounds.yMin;
        }

        public void SpawnFloor(DungeonRoom_Segment[,] Segments)
        {
            for (int x = hardPosition.x; x < hardPosition.x + Size.x; x++)
            {
                for (int y = hardPosition.y; y < hardPosition.y + Size.y; y++)
                {
                    Segments[x, y].s.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }
    }
}
