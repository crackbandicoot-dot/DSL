using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    public class Context
    {

        Dictionary<string, IDSLObject> objects;

        public Context()
        {
            objects = new Dictionary<string, IDSLObject>();
        }

        internal void Declare(IDSLObject obj)
        {
            objects.Add(obj.Name, obj);
        }
        internal IDSLObject Acced(string objName)
        {
            if (objects.TryGetValue(objName, out IDSLObject? value))
            {
                return value;
            }
            throw new Exception($"Object {objName} is not in the current context");
        }
    }
}