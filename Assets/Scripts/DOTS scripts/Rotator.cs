using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace hybrid_ecs
{
    // this header means that you can attach it to gameobjects
    [GenerateAuthoringComponent]
    public struct RotatorComponent : IComponentData
    {
        // data
        public float rotateSpeed;
    }

    public class Rotator : SystemBase
    {
        // behaviour logic

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Rotation rotation, in RotatorComponent comp) =>
            {
                rotation.Value = math.mul(
                        math.normalize(rotation.Value),
                        quaternion.AxisAngle(math.up(), -comp.rotateSpeed * deltaTime)
                    );
            }).ScheduleParallel();
        }

    }

}

