module Day6

open System
open System.IO

let private parseInput filePath =
    let currentGroup, allGroups =
        filePath
        |> File.ReadAllLines
        |> Seq.fold (fun (currentGroup, allGroups) line ->
            if String.IsNullOrWhiteSpace line then Set.empty, currentGroup::allGroups
            else
                let currentGroup =
                    line
                    |> Seq.fold (fun s t -> s |> Set.add t) currentGroup
                currentGroup, allGroups) (Set.empty, [])
    currentGroup::allGroups

let private calculate = parseInput >> Seq.map Set.count >> Seq.sum

let run filePath =
    filePath
    |> calculate
    |> printfn "Total group count: %i"

let private parseInputPart2 filePath =
    let currentGroup, allGroups =
        filePath
        |> File.ReadAllLines
        |> Seq.fold (fun (currentGroup, allGroups) line ->
            if String.IsNullOrWhiteSpace line then [], currentGroup::allGroups
            else
                let currentGroup = (Set.ofSeq line)::currentGroup
                currentGroup, allGroups) ([], [])
    currentGroup::allGroups

let private calculatePart2 = parseInputPart2 >> Seq.map (Set.intersectMany >> Set.count) >> Seq.sum

let runPart2 filePath =
    filePath
    |> calculatePart2
    |> printfn "Total: %i"