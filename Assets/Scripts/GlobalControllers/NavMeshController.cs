using UnityEngine;
using UnityEngine.AI;

namespace GlobalControllers
{
    public class NavMeshController : MonoBehaviour
    {
        public void BakeNavMesh(NavMeshSurface navMeshSurface)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
