﻿// Learn more about F# at http://fsharp.org

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
    | _ -> printfn "No match found"
    0 // return an integer exit code