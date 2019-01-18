using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orbit : MainMenu
{
    // The radius of the spheres
    public int maxRadius = 200;

    public GameObject[] spheres;
    public Material[] mats;
    public Material trailMat;

   
    /* Method will take the number of spheres, and also the total radius. Will create each instance of the planet,
     * which will also set its position in space */
    void Start()
    {
        spheres = CreateSpheres(planetnumb, maxRadius);
    }

    private void Awake()
    {
        spheres = new GameObject[planetnumb];
    }

    public GameObject[] CreateSpheres(int count, int radius)
    {
        /* Creating a temporary array of spheres that will populate the simulation area, and we will 
         * return the amount of spheres that were created*/
        var sphs = new GameObject[count];

        /* Then will create a sphereToCopy which will be our prefab here, instead of one that will be in our assets.
         * Using a PrimitiveType, which is just our type of sphere.*/
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        /* We need a rigidbody as we want to be able to push the object around by utilizing force, and with the physics 
         * system that is implemented into Unity. So here we call our prefab and add the component on Rigidbody to it.*/
        Rigidbody rb = sphereToCopy.AddComponent<Rigidbody>();

        // Here we are turning off the gravity of the rigidbody that we had just assigned above.
        rb.useGravity = false;

        // We will be looping through our array here
        for (int i = 0; i < count; i++)
        {
            // This will instantiate (copy or clone sphereToCopy). 
            var sp = GameObject.Instantiate(sphereToCopy);

            /* The orbiting planet's position is set and it will be equal to the position of the Sun (centre planet).
             * They are made equal because we want the spheres to be around, relative per se, the Sun. */
            sp.transform.position = this.transform.position +
                                        /* To the Suns position we are adding a Vector3, which is going to be random in its location.
                                         * Since Vector3 contains components equal to (x, y, z), they are initialized below in respective order.
                                         * The X component will be random and will be between -maxRadius and +maxRadius.
                                         * The Y component will be random and be restricted to be between -radiusnumb and +radiusnumb.
                                         * The X component will be random and will be between -maxRadius and +maxRadius.
                                         * These 3 lines of code control a flattened cube shape that will encapsulate the transform
                                         * position of the Sun. The reason behind this is to make a disk-like shape of the planets that will
                                         * orbit around.*/
                                        new Vector3(Random.Range(-maxRadius, maxRadius),
                                                    Random.Range(-radiusnumb, radiusnumb),
                                                    Random.Range(-maxRadius, maxRadius));
            /* Local scale allows you to create different sized planets, and the scale of those planets will have a random value assigned to them
             * that will range anywhere from half, to five.*/
            sp.transform.localScale *= Random.Range(0.5f, 5);

            /* Here is our sphere's renderer. This is where we access our material array, and assign one of our materials to this sphere.
             * It randomly chooses an index, which begins begins at index 0 and ranges to the total length of the Mats array.*/
            sp.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
           
            /* Since the sphere itself is coded in, and not manually added into Unity, we must manually added in a trail.
             * We create a trail renderer and add it as a component of the sphere. */
            TrailRenderer tr = sp.AddComponent<TrailRenderer>();

            // Used to set how long the trail will be visible
            tr.time = 2.0f;

            // Sets the width of the trail at the position in which it is attached to the planet
            tr.startWidth = 1f;

            // Sets the width at the end of the trial, at the time 1.0f
            tr.endWidth = 0;

            // Sets the material of the trail itself
            tr.material = trailMat;

            // The color of the trail at the position where it is attached to the planet
            tr.startColor = new Color(1, 1, 0, 0.1f);
        
            // The color of the trail at the end of the trail
            tr.endColor = new Color(0, 0, 0, 0);
     
            // Assigns the spheres to the spheres array
            spheres[i] = sp;

            AudioSource audioSource = sp.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("whoosh") as AudioClip;
            audioSource.Play();
        }

        // Destroys the prefab copy of the sphere's we created.
        GameObject.Destroy(sphereToCopy);

        // Returns the spheres, which will go back at the Start() function.
        return spheres;
    }
    // Update is called once per frame
    void Update()
    {
        // We will looping around each of the spheres that were generated
        foreach (GameObject s in spheres)
        {
            /* Since we will be needing to use Newtons Law of Gravitation, we will be needing to calculate the unknown variables.
             * Here we are calculating the difference in distance between the centre of the Sun and any orbiting planet.*/
            Vector3 difference = this.transform.position - s.transform.position;

            // Here we are getting the magnitude, or length, of the distance vector we just calculated above as to get the exact radius.
            float dist = difference.magnitude;

            /* We are creating a direction in which we will be travelling here. In this case we are making the spheres (planets)
             * be pulled towards the Sun. This is because the Sun itself does not have a Rigidbody component attached to it, and will not move around.*/
            Vector3 gravityDirection = difference.normalized;

            /* This is where we calculate the F (force) in the equation. 
             * The value of 6.7 is assigned as a rough estimate for the value of G.
             * G is then multiplied by the masses of the two objects that are involved (m1 and m2), which is given by the random localscale that 
             * was assigned to it before. 
             * There is no significance in finding the localScale in the X component specifically, as all the components are equivalent to another (X = Y = Z). 
             * We then multiply it by gravnumb, as the actual value of G is so insignificantly small that it wouldn't show us anything when simulated in Unity.
             * The r^2 is then calculated by multiplying the two radius' by one another.*/
            float gravity = 6.7f * (this.transform.localScale.x * s.transform.localScale.x * gravnumb) / (dist * dist);

            // To give our planets a gravtional vector, we multiply the direction by the gravitional force.
            Vector3 gravityVector = (gravityDirection * gravity);

            //We are pushing the spheres forward along their Z axis, as they need some initial force before they encounter our Sun's gravitational force.
            s.transform.GetComponent<Rigidbody>().AddForce(s.transform.forward, ForceMode.Acceleration);

            /* Using the Rigidbody component, we add the gravitational force to the spheres. 
             * The reason we add the ForceMode.Acceleration is because we are adding our own force and not utilizing Unity's built in physics system.
             * It will also insure that it will add the gravitional force to the simulation without taking into account the mass of the rigidbody,
             * as we've already assigned masses to the spheres ourselves.*/
            s.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
        }
        
    }
}
