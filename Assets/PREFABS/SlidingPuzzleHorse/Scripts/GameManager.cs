using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;


    //GIANNI Flags
    private bool gameStarted = false;
    private bool gameCompleted = false;

    //GIANNI stuff
    public GameObject hamster6;
    public GameObject hamster8;
    public GameObject hamster9;
    public GameObject hamster10;
    public GameObject antSpawner3;
    public GameObject teleportationPuzzle;

    public GameTimer gameTimer;


    // Create the game setup with size x size pieces.
    private void CreateGamePieces(float gapThickness) {
    // This is the width of each tile.
    float width = 1 / (float)size;
    for (int row = 0; row < size; row++) {
      for (int col = 0; col < size; col++) {
        Transform piece = Instantiate(piecePrefab, gameTransform);
        pieces.Add(piece);
        // Pieces will be in a game board going from -1 to +1.
        piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                          +1 - (2 * width * row) - width,
                                          0);
        piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
        piece.localRotation = Quaternion.Euler(0, 0, 0);
        piece.name = $"{(row * size) + col}";
        // We want an empty space in the bottom right.
        if ((row == size - 1) && (col == size - 1)) {
          emptyLocation = (size * size) - 1;
          piece.gameObject.SetActive(false);
        } else {
          // We want to map the UV coordinates appropriately, they are 0->1.
          float gap = gapThickness / 2;
          Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
          Vector2[] uv = new Vector2[4];
          // UV coord order: (0, 1), (1, 1), (0, 0), (1, 0)
          uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
          uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
          uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
          uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
          // Assign our new UVs to the mesh.
          mesh.uv = uv;
        }
      }
    }
  }

  // Start is called before the first frame update
  void Start() {
    pieces = new List<Transform>();
    size = 2;
    CreateGamePieces(0.01f);
        StartCoroutine(StartGame());
  }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        Shuffle();
        gameStarted = true;
    }

    public void MoveUp()
    {
        if (gameCompleted) return;  // Prevent input after game is completed
        Debug.Log("MoveUp called");
        if (emptyLocation / size != (size - 1))
        { // Prevent wrapping from the bottom row to the top
            SwapIfValid(emptyLocation + size, -size, size);
        }
    }

    public void MoveDown()
    {
        if (gameCompleted) return;
        Debug.Log("MoveDown called");
        if (emptyLocation / size != 0)
        { // Prevent wrapping from the top row to the bottom
            SwapIfValid(emptyLocation - size, +size, size);
        }
    }

    public void MoveLeft() {
        if (gameCompleted) return;
        Debug.Log("MoveLeft called");
        if (emptyLocation % size != (size - 1)) { // Prevent wrapping
            SwapIfValid(emptyLocation + 1, -1, -1);
        }
    }

    public void MoveRight() {
        Debug.Log("MoveRight called");
        if (gameCompleted) return;
        if (emptyLocation % size != 0) { // Prevent wrapping
            SwapIfValid(emptyLocation - 1, +1, -1);
        }
    }


  // Update is called once per frame
  void Update() {
    // Check for completion
    if (gameStarted && !shuffling && CheckCompletion()) {
            gameStarted = false;
    } 

    

    // On click send out ray to see if we click a piece.
    /*
    if (Input.GetMouseButtonDown(0)) {
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if (hit) {
        // Go through the list, the index tells us the position.
        for (int i = 0; i < pieces.Count; i++) {
          if (pieces[i] == hit.transform) {
            // Check each direction to see if valid move.
            // We break out on success so we don't carry on and swap back again.
            if (SwapIfValid(i, -size, size)) { break; }
            if (SwapIfValid(i, +size, size)) { break; }
            if (SwapIfValid(i, -1, 0)) { break; }
            if (SwapIfValid(i, +1, size - 1)) { break; }
          }
        }
      }
    }
    */
  }

  // colCheck is used to stop horizontal moves wrapping.
  private bool SwapIfValid(int i, int offset, int colCheck) {
    if (((i % size) != colCheck) && ((i + offset) == emptyLocation)) {
      // Swap them in game state.
      (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
      // Swap their transforms.
      (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
      // Update empty location.
      emptyLocation = i;
      Debug.Log("Swap if Valid was called with a valid move");
      return true;
    }
    Debug.Log("Swap if Valid was called, but the move was invalid");
    return false;
  }

  // We name the pieces in order so we can use this to check completion.
  private bool CheckCompletion() {
    for (int i = 0; i < pieces.Count; i++) {
      if (pieces[i].name != $"{i}") {
        return false;
      }
    }
    CompletePuzzle();
    return true;
  }

  private void CompletePuzzle()
    {
        //complete picture
        // Reactivate the last hidden piece (completing the picture)
        Transform hiddenPiece = pieces[emptyLocation];
        hiddenPiece.gameObject.SetActive(true);

        // Set correct UV coordinates for the hidden piece
        float width = 1 / (float)size;
        int row = emptyLocation / size;
        int col = emptyLocation % size;
        float gap = 0.005f;  // Slight gap to match the other pieces
        Mesh mesh = hiddenPiece.GetComponent<MeshFilter>().mesh;
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
        uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
        uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
        uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
        mesh.uv = uv;

        gameCompleted = true;

        //scene stuff
        hamster6.gameObject.SetActive(false);
        hamster8.gameObject.SetActive(false);
        hamster9.gameObject.SetActive(true);
        hamster10.gameObject.SetActive(true);
        antSpawner3.gameObject.SetActive(true);
        teleportationPuzzle.gameObject.SetActive(true);

        if (gameTimer != null) //break when game finished
        {
            gameTimer.stopRunning();
        }
        


        Debug.Log("puzzle completed");

        //hamster stuff below
    }

  private IEnumerator WaitShuffle(float duration) {
    yield return new WaitForSeconds(duration);
    Shuffle();
    shuffling = false;
  }

  // Brute force shuffling.
  public void Shuffle() {
    int count = 0;
    int last = 0;
    while (count < (size * size * size)) {
      // Pick a random location.
      int rnd = Random.Range(0, size * size);
      // Only thing we forbid is undoing the last move.
      if (rnd == last) { continue; }
      last = emptyLocation;
      // Try surrounding spaces looking for valid move.
      if (SwapIfValid(rnd, -size, size)) {
        count++;
      } else if (SwapIfValid(rnd, +size, size)) {
        count++;
      } else if (SwapIfValid(rnd, -1, 0)) {
        count++;
      } else if (SwapIfValid(rnd, +1, size - 1)) {
        count++;
      }
    }
  }
}
