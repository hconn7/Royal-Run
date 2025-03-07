using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
  [SerializeField] int startingChunkAmount = 12;
  [SerializeField] GameObject chunkPrefab;
  [SerializeField] Transform chunkParent;
  [SerializeField] float chunkLength = 10f;

 
  List<GameObject> chunks = new List<GameObject>();
  [SerializeField] float chunkMoveSpeed = 8f;

    void Start()
    {
       SpawnStartingChunks();
    }

    void Update()
    {
        
        MoveChunks();
    }




    private void SpawnStartingChunks(){
        for (int i = 0; i < startingChunkAmount; i++)
            SpawnChunk();
    }

    private void SpawnChunk()
    {
        float spawnPosZ = SpawnPosZ();

        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPosZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }




    private float SpawnPosZ()
    {
        float spawnPosZ;

        if (chunks.Count == 0)
        {
            spawnPosZ = transform.position.z;

        }
        else
            spawnPosZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        return spawnPosZ;
    }

    void MoveChunks()
    {
      for (int i = 0; i < chunks.Count; i++)
      {
        GameObject chunk = chunks[i];
        chunk.transform.Translate(-transform.forward * (chunkMoveSpeed * Time.deltaTime));

        if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
        {
          chunks.Remove(chunk);
          Destroy(chunk);
          SpawnChunk();
          
        }

      }
  }

  



}

 