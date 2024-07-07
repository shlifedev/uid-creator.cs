namespace LD.Utils
{
    
    public static class UidCreator
    {
        static class UIDContext<T>
        {
            public static Dictionary<T, long> uidMap = new(); 
        }
        
        
        private static long m_uid_unit = 100000000;
        private static long m_consumedUid = m_uid_unit;
        private static Dictionary<Type, long> currentUids = new(); 

        public static long GetInitialUIDForType(System.Type type)
        { 
            if(type == null)
            {
                throw new ArgumentNullException("uid target value is null");
            }
            if (!currentUids.ContainsKey(type))
            {
                currentUids[type] = m_consumedUid;
                m_consumedUid += m_uid_unit;
            } 
            double p1 = currentUids[type]; 
            return (long)(Math.Floor(p1 / m_uid_unit) * m_uid_unit);
        }
        
        
        public static long Create<T>(T obj)
        {
              
            var type = obj?.GetType();
            if(type == null)
            {
                throw new ArgumentNullException("uid generation target value is null");
            }
            if (!currentUids.ContainsKey(type))
            {
                currentUids[type] = m_consumedUid;
                m_consumedUid += m_uid_unit;
            } 
            
            if(UidCreator.UIDContext<T>.uidMap.ContainsKey(obj))
            {
                return UidCreator.UIDContext<T>.uidMap[obj];
            }
            else
            {
                UidCreator.UIDContext<T>.uidMap[obj] = currentUids[type]; 
                currentUids[type] += 1;
                return UidCreator.UIDContext<T>.uidMap[obj];
            }
        }
    }
}
