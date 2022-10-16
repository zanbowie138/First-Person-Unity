using UnityEngine;
public class SpawnCube : MonoBehaviour
{
   public GameObject block;
   private int width = 10;
   private int height = 4;
  
   void Update()
   {
        if (Input.GetKey("t")){
            for (int y=0; y<height; ++y)
            {
                for (int x=0; x<width; ++x)
                {
                    Instantiate(block, new Vector3(x,y,0), Quaternion.identity);
                }
            }   
        }    
        if (Input.GetKeyDown("y")){
            for (int y=0; y<100; ++y)
            {
                for (int x=0; x<40; ++x)
                {
                    Instantiate(block, new Vector3(x,y+100,0), Quaternion.identity);
                }
            }   
        }    
        if (Input.GetKeyDown("u")){
            for (int y=0; y<10; ++y)
            {
                for (int x=0; x<10; ++x)
                {
                    for (int z=0; z<10; ++z)
                    {
                        Instantiate(block, new Vector3(x,y+100,z), Quaternion.identity);
                    }
                }
            }   
        }    
        if (Input.GetKey("c")) {
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
            foreach(GameObject cube in cubes) {
                Destroy(cube);
            }
        }
   }
}