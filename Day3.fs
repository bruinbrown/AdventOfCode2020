module Day3

open System.IO

type private ParsedInput =
    { TreePositions : (int * int) Set
      Width : int
      Height : int }

let private readInput filePath =
    let lines =
        filePath
        |> File.ReadAllLines
    let width = lines.[0].Length
    let height = lines.Length
    let treeLocations =
        lines
        |> Seq.indexed
        |> Seq.fold (fun s (i, t) ->
            t
            |> Seq.indexed
            |> Seq.fold (fun s (j, t) ->
                if t = '#' then Set.add (i, j) s else s
                ) s) Set.empty
    { TreePositions = treeLocations; Width = width; Height = height }

let private countTreeIntersections input (moveX, moveY) =
    seq {
        for i = 0 to (input.Height / moveY) do
            let horizontalPosition = (i * moveX) % input.Width
            let verticalPosition = i * moveY
            if input.TreePositions |> Set.contains (verticalPosition, horizontalPosition) then yield true }
    |> Seq.length
    |> int64
    
let private calculate = readInput >> countTreeIntersections

let run filePath =
    let res = calculate filePath (3,1)
    printfn "Hit tree count: %i" res

let runPart2 filePath =
    let input = readInput filePath
    let routes =
        [ (1,1); (3,1); (5,1); (7,1); (1, 2) ]

    let res =
        routes
        |> List.map (countTreeIntersections input)
        |> List.reduce ( * )
    
    printfn "Hit tree count: %i" res

