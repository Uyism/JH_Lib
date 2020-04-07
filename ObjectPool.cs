using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Jh_Lib
{
    // @HowToUse BombManager/Bomb
    /*
     * class BombManager : ObjectPool<BombManager, Bomb> 
     * {}
     * 
     * class Bomb : PoolObject
     * {}
     * 
     */

    public abstract class ObjectPool<TPool, TObject> : MonoBehaviour
        where TPool : new()
        where TObject : PoolObject, new()
    {     
        List<GameObject> mPoolList = new List<GameObject>(); // Repository

        public GameObject Push(GameObject _obj)
        {
            GameObject obj = GetSleepObject(_obj);
            obj.GetComponent<PoolObject>().ObjectAwake();
            return obj;
        }

        public void Pop(int index)
        {
            mPoolList[index]?.GetComponent<PoolObject>().ObjectSleep();
        }

        #region InnerFunction

        GameObject AssignNewObjectMemory(GameObject _obj)
        {
            GameObject obj = Instantiate(_obj);
            obj.AddComponent<PoolObject>();
            obj.GetComponent<PoolObject>().ObjectIndex = mPoolList.Count; // 생성된 순서 == 인덱스
            
            // Add to Repository
            mPoolList.Add(obj);

            return obj;
        }

        GameObject GetSleepObject(GameObject _obj)
        {
            // 1. 사용중 오브젝트 찾기
            // 2. 사용중 오브젝트 없다면 새로 생성
            foreach (GameObject pool_obj in mPoolList)
            {
                if (!pool_obj.GetComponent<PoolObject>().IsAwake)
                    return pool_obj;
            }

            GameObject obj = AssignNewObjectMemory(_obj);
            return obj;
        }
        #endregion
    }

    public class PoolObject : MonoBehaviour
    {
        bool mIsAwake = false; // 사용중 여부
        int mObjectIndex;

        public bool IsAwake { get => mIsAwake; set => mIsAwake = value; }
        public int ObjectIndex { get => mObjectIndex; set => mObjectIndex = value; }
        public void ObjectAwake() { IsAwake = true; AwakeCustome(); }
        public void ObjectSleep() { mIsAwake = false; SleepCustome(); }

        // @Need Override 
        public virtual void AwakeCustome() { }
        public virtual void SleepCustome() { }
    }
}
