module Day2

open System.IO

type private ParsedInput =
    { Range : int * int
      CharChoice : char
      Password : string }

let private isPasswordValid parsedInput =
    let min, max = parsedInput.Range
    let count = parsedInput.Password |> String.filter (fun c -> c = parsedInput.CharChoice) |> String.length
    count >= min && count <= max

let private parseRow (row: string) =
    let split = row.Split(' ')
    let range =
        let split = split.[0].Split('-')
        int split.[0], int split.[1]
    let charChoice = split.[1].Trim([| ':' |]).[0]
    let rest = split.[2]
    { Range = range; CharChoice = charChoice; Password = rest }

let private readInput inputFilePath =
    File.ReadAllLines inputFilePath
    |> Seq.map parseRow

let run filePath =
    readInput filePath
    |> Seq.filter isPasswordValid
    |> Seq.length
    |> printfn "Valid passwords: %i"

let private isPasswordValidPart2 parsedInput =
    let idx1, idx2 = parsedInput.Range
    let idx1, idx2 = idx1 - 1, idx2 - 1
    let c1, c2 = parsedInput.Password.[idx1], parsedInput.Password.[idx2]
    (c1 = parsedInput.CharChoice || c2 = parsedInput.CharChoice) && c1 <> c2

let runPart2 filePath =
    readInput filePath
    |> Seq.filter isPasswordValidPart2
    |> Seq.length
    |> printfn "Valid passwords: %i"
