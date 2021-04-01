using System;
using UnityEngine;

[Serializable]
public class TransformStateSnapshot : IStateSnapshot//, IGameNetStateSynchronizable
{
    public Vector3 position;
    public Vector3 forward;

    public TransformStateSnapshot()
    {
    }

    public TransformStateSnapshot(float posX, float posY, float posZ, float forX, float forY, float forZ)
    {
        this.position = new Vector3(posX, posY, posZ);
        this.forward = new Vector3(forX, forY, forZ);
    }

    public TransformStateSnapshot(Transform transform)
    {
        this.position = transform.position;
        this.forward = transform.forward;
    }

    //public override void GetState(Transform objectToGetStateFrom)
    //{
    //    this.posX = objectToGetStateFrom.position.x;
    //    this.posY = objectToGetStateFrom.position.y;
    //    this.posZ = objectToGetStateFrom.position.z;
    //    this.forX = objectToGetStateFrom.forward.x;
    //    this.forY = objectToGetStateFrom.forward.y;
    //    this.forZ = objectToGetStateFrom.forward.z;
    //}
    //
    //public override void SetState(Transform objectToSetStateTo)
    //{
    //    objectToSetStateTo.position = new Vector3(this.posX, this.posY, this.posZ);
    //    objectToSetStateTo.forward = new Vector3(this.forX, this.forY, this.forZ);
    //}

    //public void DeserializeNetState(BinaryReader binaryReader)
    //{
    //    this.posX = binaryReader.ReadSingle();
    //    this.posY = binaryReader.ReadSingle();
    //    this.posZ = binaryReader.ReadSingle();
    //
    //    this.forX = binaryReader.ReadSingle();
    //    this.forY = binaryReader.ReadSingle();
    //    this.forZ = binaryReader.ReadSingle();
    //
    //    //this.transform.position = new Vector3(posX, posY, posZ);
    //    //this.transform.forward = new Vector3(fordX, fordY, fordZ);
    //}
    //
    //public void SerializeNetState(BinaryWriter binaryWriter)
    //{
    //    //Vector3 position = this.transform.position;
    //    //Vector3 forward = this.transform.forward;
    //
    //    //Serialize position
    //    binaryWriter.Write(this.posX);
    //    binaryWriter.Write(this.posY);
    //    binaryWriter.Write(this.posZ);
    //
    //    binaryWriter.Write(this.forX);
    //    binaryWriter.Write(this.forY);
    //    binaryWriter.Write(this.forZ);
    //}
}
