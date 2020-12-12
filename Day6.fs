module Day6

open System
open System.IO

let private parseInput filePath =
    let currentGroup, allGroups =
        filePath
        |> File.ReadAllLines
        |> Seq.fold (fun (currentGroup, allGroups) line ->
            if String.IsNullOrWhiteSpace line then [], currentGroup::allGroups
            else
                let currentGroup = (Set.ofSeq line)::currentGroup
                currentGroup, allGroups) ([], [])
    currentGroup::allGroups

let private calculate = parseInput >> Seq.map (Set.unionMany >> Set.count) >> Seq.sum

let run filePath =
    filePath
    |> calculate
    |> printfn "Total group count: %i"

let private calculatePart2 = parseInput >> Seq.map (Set.intersectMany >> Set.count) >> Seq.sum

let runPart2 filePath =
    filePath
    |> calculatePart2
    |> printfn "Total: %i"