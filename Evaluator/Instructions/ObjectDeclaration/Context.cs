using DSL.Evaluator.LenguajeTypes;


namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    public class Context
    {

        Dictionary<LenguajeTypes.String, IDSLObject> objects;

        public Context()
        {
            objects = new Dictionary<LenguajeTypes.String, IDSLObject>();
        }

        internal void Declare(IDSLObject obj)
        {
            objects.Add(obj.Name, obj);
        }
        internal IDSLObject Acced(LenguajeTypes.String objName)
        {
            if (objects.TryGetValue(objName, out IDSLObject? value))
            {
                return value;
            }
            throw new Exception($"Object {objName} is not in the current context");
        }
    }
}