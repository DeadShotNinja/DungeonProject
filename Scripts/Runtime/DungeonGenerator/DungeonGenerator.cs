using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace DP.Runtime
{
    [Serializable]
    public class DungeonRoom_Segment
    {
        public GameObject s;
    }

    public class DungeonGenerator : MonoBehaviour
    {
        // base room generation
        [SerializeField]
        private int RandomRooms = 20;
        [SerializeField]
        private Vector2Int RandomRoomSize = new Vector2Int(1, 5);

        [SerializeField]
        private DungeonRoom DungeonRoom;

        private DungeonRoom[] Rooms;

        private Coroutine dungeonGeneration;

        private int maxIterationCount = 2000;

        public GameObject segment;


        // segmentation
        private DungeonRoom_Segment[,] Segments;
        private BoundsInt MapBounds;


        [Button("Generate")]
        private void GenerateSequenceButton()
        {
            this.GenerateSequence();
        }
        [Button("Clear")]
        private void ClearButton()
        {
            this.Clear();
        }

        void GenerateSequence()
        {
            if (dungeonGeneration != null) StopCoroutine(dungeonGeneration);
            dungeonGeneration = StartCoroutine(GenerateDungeonSeqience());
        }

        void Clear()
        {
            while (transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        DungeonRoom SpawnRandomRoom()
        {
            var position = UnityEngine.Random.insideUnitSphere * 10;
            position.x = Mathf.RoundToInt(position.x);
            position.y = 0.0f;
            position.z = Mathf.RoundToInt(position.z);

            Vector2Int roomSize = Vector2Int.zero;
            roomSize.x = UnityEngine.Random.Range(RandomRoomSize.x, RandomRoomSize.y);
            roomSize.y = UnityEngine.Random.Range(RandomRoomSize.x, RandomRoomSize.y);

            var room = Instantiate(DungeonRoom, position, Quaternion.identity, transform);
            room.Setup(roomSize);

            return room;
        }

        bool isCollided_TryMove(int id)
        {
            bool collided = false;
            for (int r = 0; r < Rooms.Length; r++)
            {
                if (Rooms[id] != Rooms[r])
                {
                    var isCollided = Rooms[id].isMoveOnCollide(Rooms[r]);
                    if (isCollided) collided = true;
                }
            }

            return collided;
        }

        void CalculateBounds()
        {
            MapBounds = Rooms[0].CalculateBounds();
            for (int id = 1; id < RandomRooms; id++)
            {
                var tempBound = Rooms[id].CalculateBounds();
                if (tempBound.xMin < MapBounds.xMin) MapBounds.xMin = tempBound.xMin;
                if (tempBound.yMin < MapBounds.yMin) MapBounds.yMin = tempBound.yMin;
                if (tempBound.xMax > MapBounds.xMax) MapBounds.xMax = tempBound.xMax;
                if (tempBound.yMax > MapBounds.yMax) MapBounds.yMax = tempBound.yMax;
            }
        }

        void NormalizeBoundsAndRooms()
        {
            Vector2Int MapSize = Vector2Int.zero;
            MapSize.x = MapBounds.xMax - MapBounds.xMin;
            MapSize.y = MapBounds.yMax - MapBounds.yMin;
            Segments = new DungeonRoom_Segment[MapSize.x + 1, MapSize.y + 1];

            for (int x = 0; x < Segments.GetLength(0); x++)
                for (int y = 0; y < Segments.GetLength(1); y++)
                    Segments[x, y] = new DungeonRoom_Segment();

            for (int id = 0; id < Rooms.Length; id++)
                Rooms[id].UpdateBoundedPosition(MapBounds);
        }


        IEnumerator GenerateDungeonSeqience()
        {
            if (transform.childCount != 0) Clear();
            Rooms = new DungeonRoom[RandomRooms];

            for (int i = 0; i < RandomRooms; i++)
            {
                yield return new WaitForEndOfFrame();
                Rooms[i] = SpawnRandomRoom();
            }

            yield return new WaitForEndOfFrame();

            bool isCollided = false;
            for (int i = 0; i < maxIterationCount; i++)
            {
                isCollided = false;
                for (int id = 0; id < Rooms.Length; id++)
                {
                    if (isCollided_TryMove(id))
                    {
                        yield return new WaitForEndOfFrame();
                        Rooms[id].UpdatePosition();
                        isCollided = true;
                    }
                }
            }
            if (isCollided) GenerateSequence();//regenerate if cant fit rooms

            for (int id = 0; id < Rooms.Length; id++)
            {
                Rooms[id].UpdatePosition();
            }
            yield return new WaitForEndOfFrame();

            CalculateBounds();
            NormalizeBoundsAndRooms();

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                yield return new WaitForEndOfFrame();
                for (int y = 0; y < Segments.GetLength(1); y++)
                    Segments[x, y].s = Instantiate(segment, new Vector3(x, 0, y), Quaternion.identity, transform);
            }

            for (int id = 0; id < Rooms.Length; id++)
            {
                yield return new WaitForEndOfFrame();
                Rooms[id].UpdatePosition();
                Rooms[id].SpawnFloor(Segments);
            }
        }

    }
}
