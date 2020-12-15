// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    match argv with
    | [| "Day1"; "Part1"; filePath  |] -> Day1.run filePath
    | [| "Day1"; "Part2"; filePath  |] -> Day1.runPart2 filePath
    | [| "Day2"; "Part1"; filePath |] -> Day2.run filePath
    | [| "Day2"; "Part2"; filePath |] -> Day2.runPart2 filePath
    | [| "Day3"; "Part1"; filePath |] -> Day3.run filePath
    | [| "Day3"; "Part2"; filePath |] -> Day3.runPart2 filePath
    | [| "Day4"; "Part1"; filePath |] -> Day4.run filePath
    | [| "Day4"; "Part2"; filePath |] -> Day4.runPart2 filePath
    | [| "Day5"; "Part1"; filePath |] -> Day5.run filePath
    | [| "Day5"; "Part2"; filePath |] -> Day5.runPart2 filePath
    | [| "Day6"; "Part1"; filePath |] -> Day6.run filePath
    | [| "Day6"; "Part2"; filePath |] -> Day6.runPart2 filePath
    | [| "Day7"; "Part1"; filePath |] -> Day7.run filePath
    | [| "Day7"; "Part2"; filePath |] -> Day7.runPart2 filePath
    | [| "Day8"; "Part1"; filePath |] -> Day8.run filePath
    | [| "Day8"; "Part2"; filePath |] -> Day8.runPart2 filePath
    | [| "Day9"; "Part1"; filePath |] -> Day9.run filePath
    | [| "Day9"; "Part2"; filePath |] -> Day9.runPart2 filePath
    | _ -> printfn "No match found"
    0 // return an integer exit code
