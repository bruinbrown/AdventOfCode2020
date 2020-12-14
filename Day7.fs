module Day7

open System
open System.IO

type Bag = Bag of string * string

let parseLine (line: string) =
    let parentChild = line.Split("contain")
    let parent = 
        let parts = parentChild.[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
        Bag(parts.[0], parts.[1])

    let children = 
        if parentChild.[1] = "no other bags." then []
        else
            parentChild.[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun t ->
                let parts = t.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                Bag(parts.[1], parts.[2]))
            |> List.ofArray

    children |> List.map (fun t -> t, parent)

let parseInput filePath =
    filePath
    |> File.ReadAllLines
    |> Seq.collect parseLine
    |> Seq.groupBy fst
    |> Seq.map (fun (c, ps) -> c, ps |> Seq.map snd |> Seq.toList)
    |> Map.ofSeq

let find (bag: Bag) (map:Map<Bag, Bag list>) =
    let rec _walk newBag =
        match Map.tryFind newBag map with
        | None -> Set.ofList [newBag]
        | Some [] -> Set.ofList [newBag]
        | Some l ->
            l
            |> List.map _walk
            |> Set.unionMany
            |> Set.union (Set.ofList l)
    _walk bag

let parseLine2 (line: string) =
    let parentChild = line.Split("contain")
    let parent = 
        let parts = parentChild.[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
        Bag(parts.[0], parts.[1])

    let children = 
        if parentChild.[1] = " no other bags." then []
        else
            parentChild.[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun t ->
                let parts = t.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                let count = parts.[0] |> int
                count, Bag(parts.[1], parts.[2]))
            |> List.ofArray

    parent, children

let run filePath =
    filePath
    |> parseInput
    |> find (Bag("shiny", "gold"))
    |> Set.count
    |> printfn "Total bags: %i"

let findChildrenBags (bag: Bag) (map: Map<Bag, (int * Bag) list>) =
    let rec _walk bag =
        let children = map.[bag]
        match children with
        | [] ->
            1
        | children ->
            let childCount =
                children
                |> List.map (fun (cnt, bag) ->
                    let childCount = _walk bag
                    childCount * cnt)
                |> List.sum
            1 + childCount
    _walk bag - 1

let runPart2 filePath =
    filePath
    |> File.ReadAllLines
    |> Seq.map parseLine2
    |> Map.ofSeq
    |> findChildrenBags (Bag("shiny", "gold"))
    |> printfn "You need %i bags"