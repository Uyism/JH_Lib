using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jh_Lib
{
    public class SingleTon<T> where T : new()
    {
        // 인스턴스 숨김
        private static T mInstance;

        // 생성자 노출
        // 생성자 호출 시 static으로 생성한 인스턴스만 반환
        public static T Instance()
        {
            if (mInstance == null)
            {
                mInstance = new T();
                return mInstance;
            }
            return mInstance;
        }
    }

    // @MonoBehaviour 상속 받는 싱글톤
    public class MonoSingleTon<T> : MonoBehaviour where T : class
    {
        private static T mInstance;

        public static T Instance()
        {
            if (mInstance == null)
                mInstance = FindObjectOfType(typeof(T)) as T;

            return mInstance;
        }
    }
}
