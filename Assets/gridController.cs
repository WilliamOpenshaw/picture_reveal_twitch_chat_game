using UnityEngine;

public class gridController : MonoBehaviour
{   
    //prefab for the grid cell image
    public GameObject cellPrefab; // Prefab for the grid object

    public GameObject cursor; // Prefab for the grid object

    public int cursorPosX = 0;

    public int cursorPosY = 0;
    public RectTransform cursorRect;

    public GameObject cellSizePrefab; // Prefab for the grid object
    // gameobject of hidden ui image to that will be covered in grid of ui image cells
    public GameObject hiddenImage; // UI image to be covered by the grid
    public int gridSizeX = 12; // Size of the grid in the X direction  
    public int gridSizeY = 12; // Size of the grid in the Y direction

    public int prevGridSizeX = 12; // Size of the grid in the X direction  
    public int prevGridSizeY = 12; // Size of the grid in the Y direction
    public float cellSizeX = 0f; // Size of each cell in the grid
    public float cellSizeY = 0f; // Size of each cell in the grid
    public float testCellSizeX = 0f; // Size of each cell in the grid
    public float testCellSizeY = 0f; // Size of each cell in the grid
    public GameObject[,] grid; // 2D array to hold the grid objects
    public RectTransform cellRectTransform; // RectTransform of the cell prefab
    public RectTransform rectTransform; // RectTransform of the cell prefab
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float sizeDeltaX = 0f; // Size of each cell in the grid
    public float sizeDeltaY = 0f; // Size of each cell in the grid
    void Start()
    {
        gridSizeX = 12; // Size of the grid in the X direction  
        gridSizeY = 12; // Size of the grid in the Y direction
        FirstDrawGrid(); // Call the DrawGrid method to create the grid        
    }

    // Update is called once per frame
    void Update()
    {
        cursorPosX = Mathf.RoundToInt((cursorRect.anchoredPosition.x/Screen.width) * gridSizeX); // Calculate the cursor position in the X direction
        cursorPosY = Mathf.RoundToInt((cursorRect.anchoredPosition.y/Screen.height) * -gridSizeY); // Calculate the cursor position in the Y direction
        // if D key is pressed, move cursor gameobject rect transform to the right
        if (Input.GetKeyDown(KeyCode.D))
        {
            //if anchored position is within screen bounds
            if (cursorPosX < gridSizeX - 1)
            {
                cursorRect.anchoredPosition += new Vector2(cellSizeX, 0);
            }            
        }
        // if A key is pressed, move cursor gameobject rect transform to the left
        if (Input.GetKeyDown(KeyCode.A))
        {
            //if anchored position is within screen bounds
            if (cursorPosX > 0)
            {
                cursorRect.anchoredPosition += new Vector2(-cellSizeX, 0);
            }            
        }
        // if W key is pressed, move cursor gameobject rect transform up
        if (Input.GetKeyDown(KeyCode.W))
        {
            //if anchored position is within screen bounds
            //if (cursorRect.anchoredPosition.y >= -Screen.height + (sizeDeltaY/2))
            if (cursorPosY > 0)
            {
                cursorRect.anchoredPosition += new Vector2(0, cellSizeY);
            }            
        }
        // if S key is pressed, move cursor gameobject rect transform down
        if (Input.GetKeyDown(KeyCode.S))
        {
            //if anchored position is within screen bounds
            if (cursorPosY < gridSizeY - 1)
            {
                cursorRect.anchoredPosition += new Vector2(0, -cellSizeY);
            }            
        }

        // if p key is pressed, drawgrid
        if (Input.GetKeyDown(KeyCode.P))
        {
            //destroy all grid cells in grid array
            for (int x = 0; x < prevGridSizeX; x++)
            {
                for (int y = 0; y < prevGridSizeY; y++)
                {
                    Destroy(grid[x, y]); // Destroy the grid object at the cursor position
                    grid[x, y] = null; // Set the reference to null after destroying
                }
            }
            grid = null; // Set the reference to null after destroying
            
            DrawGrid(); // Call the DrawGrid method to create the grid        
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            // set cursor to random position in grid
            cursorRect.anchoredPosition = new Vector2(Random.Range(0, gridSizeX) * cellSizeX, Random.Range(0, gridSizeY) * -cellSizeY);
        }

        // Destroy the grid object at the cursor position
        if (cursorPosX >= 0 && cursorPosX < gridSizeX && cursorPosY >= 0 && cursorPosY < gridSizeY)
        {
            Destroy(grid[cursorPosX, cursorPosY]);
            grid[cursorPosX, cursorPosY] = null; // Set the reference to null after destroying
        }    

        // if press U key, reload scene
        if (Input.GetKeyDown(KeyCode.U))
        {
            // reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        // if press Q key, quit application
        if (Input.GetKeyDown(KeyCode.M))
        {
            // quit application
            Application.Quit();
        }
    }
    public void DrawGrid()
    {
        cursorRect = cursor.GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();

        // cellSizeX equals the width of the ui image rect transform cell prefab
        //cellSizeX = Screen.currentResolution.width / (gridSizeX/4); // Calculate the width of each cell based on the screen resolution and grid size
        //cellSizeY = Screen.currentResolution.height / (gridSizeY/4); // Calculate the height of each cell based on the screen resolution and grid size
        // cellSizeY equals the height of the ui image rect transform cell prefab
        cellRectTransform = cellPrefab.GetComponent<RectTransform>();
        //cellSizeX = cellRectTransform.sizeDelta.x; // Get the width of the cell prefab
        //cellSizeY = cellRectTransform.sizeDelta.y; // Get the height of the cell prefab
        cellSizeX = Screen.width/gridSizeX; // Get the width of the cell prefab
        cellSizeY = Screen.height/gridSizeY; // Get the height of the cell prefab    

        sizeDeltaX = cellSizeX - (Screen.width/10); // Calculate the width of each cell based on the screen resolution and grid size
        sizeDeltaY = cellSizeY - (Screen.height/10); // Calculate the height of each cell based on the screen resolution and grid size`

        // set size delta of cursor rect
        cursorRect.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY); // Set the size of the cursor rect transform to the cell size 
        //cursorRect.anchoredPosition += new Vector2((cursorPosX * cellSizeX) + (sizeDeltaX/2), (-cursorPosY * cellSizeY) - (sizeDeltaY/2)); // Set the anchored position of the cursor rect transform to the calculated position    
        //cursorRect.anchoredPosition += new Vector2(cursorPosX * cellSizeX, -cursorPosY * cellSizeY); // Set the anchored position of the cursor rect transform to the calculated position    
        cursorRect.anchoredPosition = new Vector2((Random.Range(0, gridSizeX) * cellSizeX) + (sizeDeltaX/2), (Random.Range(0, gridSizeY) * -cellSizeY) - (sizeDeltaY/2));
      
        
        //cover the hidden image with grid of ui image cells
        grid = new GameObject[gridSizeX, gridSizeY]; // Initialize the grid array
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Calculate the position of each cell in the grid
                Vector3 position = new Vector3(0, 0, 0);
                // Create a new grid object at the calculated position
                GameObject gridCell = Instantiate(cellPrefab, position, Quaternion.identity);
                // Set the parent of the grid object to the hidden image
                gridCell.transform.SetParent(hiddenImage.transform);
                // Set the ui image anchored position of the grid object to the calculated position
                rectTransform = gridCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * cellSizeX) + (sizeDeltaX/2), (-y * cellSizeY) - (sizeDeltaY/2)); // Set the anchored position of the grid object
                // Set the size of the grid object to the specified cell size
                rectTransform.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
                // Set the name of the grid object for easier identification in the hierarchy
                gridCell.name = "GridCell_" + x + "_" + y;
                // Set the color of the grid object to a random color
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                gridCell.GetComponent<UnityEngine.UI.Image>().color = randomColor;
                // Store the reference to the grid object in the array
                grid[x, y] = gridCell;
            }
            prevGridSizeX = gridSizeX; // Store the previous grid size in the X direction
            prevGridSizeY = gridSizeY; // Store the previous grid size in the Y direction
        }
        //cellPrefab.SetActive(false); // Hide the cell prefab in the hierarchy        
    }
    public void FirstDrawGrid()
    {
        cursorRect = cursor.GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();

        // cellSizeX equals the width of the ui image rect transform cell prefab
        //cellSizeX = Screen.currentResolution.width / (gridSizeX/4); // Calculate the width of each cell based on the screen resolution and grid size
        //cellSizeY = Screen.currentResolution.height / (gridSizeY/4); // Calculate the height of each cell based on the screen resolution and grid size
        // cellSizeY equals the height of the ui image rect transform cell prefab
        cellRectTransform = cellPrefab.GetComponent<RectTransform>();
        //cellSizeX = cellRectTransform.sizeDelta.x; // Get the width of the cell prefab
        //cellSizeY = cellRectTransform.sizeDelta.y; // Get the height of the cell prefab
        cellSizeX = Screen.width/gridSizeX; // Get the width of the cell prefab
        cellSizeY = Screen.height/gridSizeY; // Get the height of the cell prefab    

        sizeDeltaX = cellSizeX - (Screen.width/10); // Calculate the width of each cell based on the screen resolution and grid size
        sizeDeltaY = cellSizeY - (Screen.height/10); // Calculate the height of each cell based on the screen resolution and grid size`

        // set size delta of cursor rect
        cursorRect.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY); // Set the size of the cursor rect transform to the cell size 
        //cursorRect.anchoredPosition += new Vector2((cursorPosX * cellSizeX) + (sizeDeltaX/2), (-cursorPosY * cellSizeY) - (sizeDeltaY/2)); // Set the anchored position of the cursor rect transform to the calculated position    
        //cursorRect.anchoredPosition += new Vector2(cursorPosX * cellSizeX, -cursorPosY * cellSizeY); // Set the anchored position of the cursor rect transform to the calculated position    
        cursorRect.anchoredPosition = new Vector2((Random.Range(0, gridSizeX) * cellSizeX) + (sizeDeltaX/2), (Random.Range(0, gridSizeY) * -cellSizeY) - (sizeDeltaY/2));
      

        //cover the hidden image with grid of ui image cells
        grid = new GameObject[gridSizeX, gridSizeY]; // Initialize the grid array
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Calculate the position of each cell in the grid
                Vector3 position = new Vector3(0, 0, 0);
                // Create a new grid object at the calculated position
                GameObject gridCell = Instantiate(cellPrefab, position, Quaternion.identity);
                // Set the parent of the grid object to the hidden image
                gridCell.transform.SetParent(hiddenImage.transform);
                // Set the ui image anchored position of the grid object to the calculated position
                rectTransform = gridCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * cellSizeX) + (sizeDeltaX/2), (-y * cellSizeY) - (sizeDeltaY/2)); // Set the anchored position of the grid object
                // Set the size of the grid object to the specified cell size
                rectTransform.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
                // Set the name of the grid object for easier identification in the hierarchy
                gridCell.name = "GridCell_" + x + "_" + y;
                // Set the color of the grid object to a random color
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                gridCell.GetComponent<UnityEngine.UI.Image>().color = randomColor;
                // Store the reference to the grid object in the array
                grid[x, y] = gridCell;
            }
            prevGridSizeX = gridSizeX; // Store the previous grid size in the X direction
            prevGridSizeY = gridSizeY; // Store the previous grid size in the Y direction
        }
        //cellPrefab.SetActive(false); // Hide the cell prefab in the hierarchy        
    }
}
