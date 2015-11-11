open System
open System.IO

type Shot =
    { X  : float
      Y  : float
      Speed    : float
      ExpectedDistance : float
      Name     : string }

type ShotMod =
    { Distance : float
      MaxDistance : float
      Expected : float
      Success  : bool
      Angle    : float
      Name     : string }

let gravity = 9.81

let calculation shot =
    let { X = x; Y = y; Speed = speed; ExpectedDistance = distance; Name = name } = shot
    let maxDistance = speed ** 2.0 / gravity
    let theta = atan (y/x)
    let angleOfReach = 0.5 * asin (gravity * distance / speed ** 2.0)
    let distanceTravelled = speed**2.0 * sin(2.0 * theta) / gravity
    let hit = distanceTravelled = distance
    let calculatedShot = { Distance = distanceTravelled; MaxDistance = maxDistance; Expected = distance; Success = hit; Angle = angleOfReach; Name = name }
    calculatedShot

let result shot =
    let calc = calculation shot
    match calc with
    | { ShotMod.Distance = x; ShotMod.MaxDistance = max; ShotMod.Expected = ed ; ShotMod.Success = hit; ShotMod.Angle = angle; ShotMod.Name = name } when ed > max -> sprintf "%s: miss; distance out of reach" name
    | { ShotMod.Distance = x; ShotMod.MaxDistance = max; ShotMod.Expected = ed ; ShotMod.Success = hit; ShotMod.Angle = angle; ShotMod.Name = name } when hit      -> sprintf "%s: hit" name
    | { ShotMod.Distance = x; ShotMod.MaxDistance = max; ShotMod.Expected = ed ; ShotMod.Success = hit; ShotMod.Angle = angle; ShotMod.Name = name } when not hit  -> sprintf "%s: miss; required angle of reach: %f" name angle
    | _ -> ""

let GetFile =
    let mutable filepath = ""
    while filepath = "" do
        Console.Write("Enter the full path to the name of the input file: ")
        filepath <- Console.ReadLine()
    filepath

let exit (msg: string) exitCode =
    printfn ""
    printfn "%s %s" msg "Press a key to quit."
    Console.ReadKey() |> ignore
    exitCode

[<EntryPoint>]
let main argv =
    try
        use input =
            new StreamReader(match argv.Length with
                             | 0 -> GetFile
                             | _ -> argv.[0])

        let shots =
            [ while not input.EndOfStream do
                  let raw = input.ReadLine()
                  let values = raw.Split(',')
                  yield { X  = float values.[0]
                          Y  = float values.[1]
                          Speed    = float values.[2]
                          ExpectedDistance = float values.[3]
                          Name  = values.[4] } ]

        let shotsfired =
            seq {
                for p in shots ->
                    result p
            }

        Console.WriteLine("The shots fired were: ")
        for e in shotsfired do
            Console.WriteLine(e)

        exit "" 0
    with
    | :? System.IO.FileNotFoundException ->
        exit "File Not Found. " -1
    | _ ->
        exit "Program failure. " -1
