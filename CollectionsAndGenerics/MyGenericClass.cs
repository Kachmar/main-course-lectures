using System;

namespace CollectionsAndGenerics
{
    class MyGenericClass<THuman> where THuman : IHuman
    {
        private THuman genericMemberVariable;

        public MyGenericClass(THuman value)
        {
            genericMemberVariable = value;
        }

        public THuman genericMethod(THuman genericParameter)
        {
            Console.WriteLine("Parameter type: {0}, value: {1}", typeof(THuman).ToString(), genericParameter);
            Console.WriteLine("Return type: {0}, value: {1}", typeof(THuman).ToString(), genericMemberVariable);

            return genericMemberVariable;
        }

        public THuman genericProperty { get; set; }
    }
}