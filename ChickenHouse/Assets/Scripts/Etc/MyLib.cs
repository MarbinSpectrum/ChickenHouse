using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace MyLib
{
    public static class Exception
    {
        #region[�迭 �����ʰ� �˻�]
        public static bool IndexOutRange<T>(int x, int y, T[,] array)
        {
            if (x >= array.GetLength(0) || x < 0 || y >= array.GetLength(1) || y < 0)
                return false;
            return true;
        }

        public static bool IndexOutRange<T>(Vector2Int v, T[,] array)
        {
            return IndexOutRange<T>(v.x, v.y, array);
        }

        public static bool IndexOutRange<T>(int a, List<T> array)
        {
            if (array == null || a >= array.Count || a < 0)
                return false;
            return true;
        }

        public static bool IndexOutRange<T>(int a, T[] array)
        {
            if (a >= array.GetLength(0) || a < 0)
                return false;
            return true;
        }
        #endregion
    }

    public static class AreaCheck
    {
        #region[���� �������� �˻�]
        public static bool RectIn(Vector2 pos, Rect rect)
        {
            if (rect.x > pos.x || rect.x + rect.width < pos.x || rect.y < pos.y || rect.y - rect.height > pos.y)
                return false;
            return true;
        }

        public static bool RectIn(Vector2 pos, RectInt rect)
        {
            return RectIn(pos, new Rect(rect.x, rect.y, rect.width, rect.height));
        }
        #endregion
    }

    #region[PriorityQueue]
    public class PriorityQueue<T> where T : IComparable<T>
    {
        // �� Ʈ���� �迭�� ������ �� �ִ�.
        List<T> _heap = new List<T>();

        public void Push(T data)
        {
            // ���� �� ���� ���ο� �����͸� �����Ѵ�.
            _heap.Add(data);

            int now = _heap.Count - 1;  // �߰��� ����� ��ġ. ���� �� ������ ����.

            // ���� ���� ���� ����
            while (now > 0)
            {
                int next = (now - 1) / 2;  // �θ� ���
                if (_heap[now].CompareTo(_heap[next]) < 0)  // �θ� ���� ��
                    break;

                // �� ���� ���� �ڸ� �ٲ�
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // �˻� ��ġ�� �̵��Ѵ�.
                now = next;
            }
        }

        public T Pop()  // �ִ밪(��Ʈ)�� �̾Ƴ���.
        {
            // ��ȯ�� �����͸� ���� ����
            T ret = _heap[0];

            // ������ �����͸� ��Ʈ�� �̵���Ų��.
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // �Ʒ��� ���� ���� ����
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // ���� ���� ���簪���� ũ��, �������� �̵�
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;
                // ������ ���� ���簪(���� �̵� ����)���� ũ��, ���������� �̵�
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                // ����/������ ��� ���簪���� ������ ����
                if (next == now)
                    break;

                // �� �� ���� �ڸ� �ٲ�
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // �˻� ��ġ�� �̵��Ѵ�.
                now = next;
            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }
    #endregion

    public static class Algorithm
    {
        #region[Next_Permutation]
        public static bool Next_Permutation<T>(List<T> list) where T : IComparable
        {
            Action<int, int> Swap = (int idx1, int idx2) => { T temp = list[idx1]; list[idx1] = list[idx2]; list[idx2] = temp; };
            int a = 0, b = 0, p = 0; //p : pivot
            for (int i = list.Count - 2; i >= 0; --i)
                if (list[i].CompareTo(list[i + 1]) < 0)
                {
                    a = i;
                    p = i + 1;

                    for (int j = list.Count - 1; j >= 0; --j)
                        if (list[a].CompareTo(list[j]) < 0)
                        {
                            b = j;
                            break;
                        }

                    Swap(a, b);

                    for (int j = 0; j < (list.Count - p) / 2; j++)
                        Swap(j + p, list.Count - j - 1);

                    return true;
                }
            // �̹� ������� ���ĵǾ� ���� => �������� ����
            for (int i = 0; i < list.Count / 2; i++)
                Swap(i, list.Count - i - 1);
            return false;
        }
        #endregion

        #region[Swap]
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        #endregion

        #region[Shuffle]
        public static void Shuffle<T>(ref List<T> list)
        {
            //list�� �ִ� �����͸� ���´�.
            //�����ϰ� �ε���(a,b) �ΰ��� ���ϰ�
            //�ش� �ε����� �ش��ϴ� ���� ��ȯ�Ѵ�.
            //�ش� ������ list����*10�� ��ŭ �ݺ��Ѵ�.
            for (int i = 0; i < list.Count * 10; i++)
            {
                int a = UnityEngine.Random.Range(0, list.Count);
                int b = UnityEngine.Random.Range(0, list.Count);

                T temp = list[a];
                list[a] = list[b];
                list[b] = temp;
            }
        }
        #endregion

        #region[CreateRandomList]
        //1~N�߿��� �ߺ����� �ʴ� m���� �̴´�.
        public static List<int> CreateRandomList(int n, int m)
        {
            int[] tree = new int[n + 1];
            List<int> temp = new List<int>();

            int Sum(int i)
            {
                int ans = 0;
                while (i > 0)
                {
                    ans += tree[i];
                    i -= (i & -i);
                }
                return ans;
            }

            void Update(int i, int num)
            {
                while (i <= n)
                {
                    tree[i] += num;
                    i += (i & -i);
                }
            }

            for (int i = 1; i <= n; i++)
                Update(i, 1);

            for (int i = n; i > n - m; i--)
            {
                int rand = UnityEngine.Random.Range(0, i) + 1;

                int left = 1;
                int right = n;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (Sum(mid) >= rand)
                        right = mid;
                    else
                        left = mid + 1;
                }
                temp.Add(right);
                Update(right, -1);
            }

            return temp;
        }
        #endregion

        #region[MakeMaze]
        public static bool[,] MakeMaze(uint pWidth, uint pHeight)
        {
            uint mazeW = pWidth * 2 + 1;
            uint mazeH = pHeight * 2 + 1;

            bool[,] isWall = new bool[mazeW, mazeH];
            for (int x = 0; x < mazeW; x++)
            {
                for (int y = 0; y < mazeH; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        isWall[x, y] = true;
                    else
                        isWall[x, y] = false;
                }
            }

            for (int x = 0; x < mazeW; x++)
            {
                for (int y = 0; y < mazeH; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == mazeH - 2)
                    {
                        isWall[x + 1, y] = false;
                        continue;
                    }

                    if (x == mazeW - 2)
                    {
                        isWall[x, y + 1] = false;
                        continue;
                    }

                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        isWall[x + 1, y] = false;
                    }
                    else
                    {
                        isWall[x, y + 1] = false;
                    }
                }
            }

            for (int x = 0; x < mazeW; x++)
                for (int y = 0; y < mazeH; y++)
                    if (x == 0 || y == 0 || x == mazeW - 1 || y == mazeH - 1)
                    {
                        //�����ڸ� �渷��
                        isWall[x, y] = true;
                    }


            return isWall;
        }
        #endregion

        #region[A*]
        //pFrom���� pTo�� �̵��ϴ� ��θ� ���Ѵ�.
        public struct AstarNode : IComparable<AstarNode>
        {
            public float f;
            public float g;
            public Vector2Int pos;
            public Vector2Int parents;

            public AstarNode(float pF, float pG, Vector2Int pPos, Vector2Int pParents)
            {
                f = pF;
                g = pG;
                pos = pPos;
                parents = pParents;
            }
            public int CompareTo(AstarNode other)
            {
                return other.f.CompareTo(f);
            }
        }

        public static List<Vector2Int> AstartRoute(Vector2Int pFrom, Vector2Int pTo, bool[,] pIsWall)
        {
            //4����
            int[,] offset = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };
            int arrayW = pIsWall.GetLength(0);
            int arrayH = pIsWall.GetLength(1);
            bool[,] isClose = new bool[arrayW, arrayH];

            List<Vector2Int> route = null;
            Vector2Int[,] parent = new Vector2Int[arrayW, arrayH];

            PriorityQueue<AstarNode> pq = new PriorityQueue<AstarNode>(); //��������Ʈ
            pq.Push(new AstarNode(Mathf.Abs(pFrom.x - pTo.x) + Mathf.Abs(pFrom.y - pTo.y), 0, pFrom, pFrom));

            bool explore = false;

            while (pq.Count() > 0)
            {
                AstarNode node = pq.Pop();
                if (isClose[node.pos.x, node.pos.y])
                    continue;

                isClose[node.pos.x, node.pos.y] = true;

                parent[node.pos.x, node.pos.y] = node.parents;

                if (node.pos.x == pTo.x && node.pos.y == pTo.y)
                {
                    explore = true;
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    int ax = node.pos.x + offset[i, 0];
                    int ay = node.pos.y + offset[i, 1];
                    if (ax < 0 || ay < 0 || ax >= arrayW || ay >= arrayH)
                        continue;

                    if (pIsWall[ax, ay])
                        continue;

                    if (isClose[ax, ay])
                        continue;

                    float g = node.g + 10;
                    float h = 10 * (Mathf.Abs(ax - pTo.x) + Mathf.Abs(ay - pTo.y));

                    pq.Push(new AstarNode(g + h, g, new Vector2Int(ax, ay), node.pos)); //parent �߰�
                }
            }

            if (explore)
            {
                route = new List<Vector2Int>();
                int ax = pTo.x;
                int ay = pTo.y;

                while (ax != pFrom.x || ay != pFrom.y)
                {
                    route.Add(new Vector2Int(ax, ay));
                    int bx = ax;
                    int by = ay;
                    ax = parent[bx, by].x;
                    ay = parent[bx, by].y;
                }
            }

            return route;
        }
        #endregion
    }

    public static class Calculator
    {
        #region[CalculateHexagonPos]

        //����� ũ�⸦ ����ؼ� x,y�� �ش��ϴ� ����� ��ġ�� ���Ѵ�.
        public static Vector2 CalculateHexagonPos(float blockWidth, float blockHeight, int x, int y)
        {
            float resultY = y * blockHeight * 0.5f;
            float resultX = x * blockWidth;
            if (y % 2 != 1)
                resultX += blockWidth * 0.5f;

            return new Vector2(resultX, resultY);
        }
        #endregion

        #region[GetAroundHexagonPos]
        private static int[][,] hexagonAroundPos = new int[][,]
        {
            new int[,]{{0, 2}, {+1, +1}, {1, -1}, {0, -2}, { +0, -1}, {+0, +1}},
            new int[,]{{0, 2}, {+0, +1}, {0, -1}, {0, -2}, { -1, -1}, {-1, +1}},
        };

        //x,y�ֺ��� ���� �����ش�.
        public static List<Vector2Int> GetAroundHexagonPos(int pX, int pY)
        {
            int by = pY % 2; //y��ǥ�� ���� �ֺ� ����� ��ġ�� �ٸ���.
            List<Vector2Int> aroundList = new List<Vector2Int>();
            for (int idx = 0; idx < 6; idx++)
            {
                int newX = pX + hexagonAroundPos[by][idx, 0];
                int newY = pY + hexagonAroundPos[by][idx, 1];
                aroundList.Add(new Vector2Int(newX, newY));
            }
            return aroundList;
        }
        #endregion

        #region[GetDicHeHexagonPos]
        public static Vector2Int GetDicHeHexagonPos(int pX, int pY, float pAngle)
        {
            int by = pY % 2; //y��ǥ�� ���� �ֺ� ����� ��ġ�� �ٸ���.
            float angleR = 120;
            float angleL = 60;
            for (int idx = 0; idx < 6; idx++)
            {
                float minV = Mathf.Min(angleL, angleR);
                float maxV = Mathf.Max(angleL, angleR);
                if (minV <= pAngle && pAngle <= maxV)
                {
                    int newX = pX + hexagonAroundPos[by][idx, 0];
                    int newY = pY + hexagonAroundPos[by][idx, 1];
                    return new Vector2Int(newX, newY);
                }
                angleL -= 60;
                angleR -= 60;
                if (angleL < 0 || angleR < 0)
                {
                    angleL += 360;
                    angleR += 360;
                }
            }

            return Vector2Int.one * -1; ;
        }
        #endregion

        #region[GetAround4Pos]
        public static int[,] Around4Pos = new int[,]
        {
            {0, 1}, {1, 0}, { 0, -1}, { -1, 0}
        };
        //x,y�ֺ��� ���� �����ش�.
        public static List<Vector2Int> GetAround4Pos(int pX, int pY)
        {
            List<Vector2Int> aroundList = new List<Vector2Int>();
            for (int idx = 0; idx < 8; idx++)
            {
                int newX = pX + Around4Pos[idx, 0];
                int newY = pY + Around4Pos[idx, 1];
                aroundList.Add(new Vector2Int(newX, newY));
            }
            return aroundList;
        }
        #endregion

        #region[GetAround8Pos]
        public static int[,] Around8Pos = new int[,]
        {
            {0, 1}, {+1, +1}, {1, 0}, {1, -1}, { 0, -1}, {-1, -1}, { -1, 0}, {-1, +1}
        };
        //x,y�ֺ��� ���� �����ش�.
        public static List<Vector2Int> GetAround8Pos(int pX, int pY)
        {
            List<Vector2Int> aroundList = new List<Vector2Int>();
            for (int idx = 0; idx < 8; idx++)
            {
                int newX = pX + Around8Pos[idx, 0];
                int newY = pY + Around8Pos[idx, 1];
                aroundList.Add(new Vector2Int(newX, newY));
            }
            return aroundList;
        }
        #endregion

        #region[GetAngle]
        public static float GetAngle(Vector2 start, Vector2 end)
        {
            Vector2 v2 = end - start;
            return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        }
        #endregion
    }

    public static class Json
    {
        #region[JSON ��ƿ]
        public static string ObjectToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T JsonToOject<T>(string jsonData)
        {
            return JsonUtility.FromJson<T>(jsonData);
        }

        public static void CreateJsonFile(string fileName, string jsonData)
        {
#if UNITY_EDITOR
            string filePath = string.Format("{0}/{1}.json", Application.dataPath + "/Resources", fileName);
#else
            string filePath = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);
#endif
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            string encodeJson = Convert.ToBase64String(data);

            File.WriteAllText(filePath, encodeJson);
        }

        public static void DeleteJsonFile(string fileName)
        {
#if UNITY_EDITOR
            string filePath = string.Format("{0}/{1}.json", Application.dataPath + "/Resources", fileName);
#else
            string filePath = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);
#endif
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static T LoadJsonFile<T>(string fileName)
        {
#if UNITY_EDITOR
            string filePath = string.Format("{0}/{1}.json", Application.dataPath + "/Resources", fileName);
#else
            string filePath = string.Format("{0}/{1}.json", Application.persistentDataPath, fileName);
#endif
            if (File.Exists(filePath) == false)
                return default(T);

            string jsonFile = File.ReadAllText(filePath);
            byte[] data = Convert.FromBase64String(jsonFile);

            string decodeJson = Encoding.UTF8.GetString(data);

            return JsonUtility.FromJson<T>(decodeJson);
        }

        [System.Serializable]
        public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
        {
            [SerializeField]
            List<TKey> keys;
            [SerializeField]
            List<TValue> values;

            Dictionary<TKey, TValue> target;
            public Dictionary<TKey, TValue> ToDictionary() { return target; }

            public Serialization(Dictionary<TKey, TValue> target)
            {
                this.target = target;
            }

            public void OnBeforeSerialize()
            {
                keys = new List<TKey>(target.Keys);
                values = new List<TValue>(target.Values);
            }

            public void OnAfterDeserialize()
            {
                var count = Mathf.Min(keys.Count, values.Count);
                target = new Dictionary<TKey, TValue>(count);
                for (var i = 0; i < count; ++i)
                {
                    target.Add(keys[i], values[i]);
                }
            }
        }

        [System.Serializable]
        public class Serialization<T>
        {
            [SerializeField]
            List<T> target;
            public List<T> ToList() { return target; }

            public Serialization(List<T> target)
            {
                this.target = target;
            }
        }
#endregion
    }

    public static class Action2D
    {
#region[MoveTo]
        public static IEnumerator MoveTo(Transform target, Vector3 to, float duration)
        {
            Vector2 from = target.position;

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                elapsed += Time.smoothDeltaTime;
                target.position =
                    Vector2.Lerp(from, to, elapsed / duration);

                yield return null;
            }

            target.position = to;

            yield break;


        }
#endregion
    }
}