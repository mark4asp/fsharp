open System
 
let getNum() =
    Console.Write "Please enter an integer: "
    let couldparse, numberIn = Int32.TryParse(Console.ReadLine())
    (couldparse, numberIn)

let askForValue() =
    let c, n = getNum()
    let mutable couldparse = c
    let mutable numberIn = n
    while not couldparse do
        Console.Write "Incorrect value. "
        let c, n = getNum()
        couldparse <- c
        numberIn <- n
    numberIn
 
let anotherValue() =
    Console.Write("Would you like add another value? (y/n):")
    Console.ReadLine().ToLower()

let rec enterValues values =
    if anotherValue() = "y"
    then askForValue() :: values |> enterValues
    else values

let output xs =
    Console.WriteLine()
    printfn "number | product with phi"
    for i in xs do
        let num, phi = i
        printfn "    %d  | %f" num phi

[<EntryPoint>]
    let main argv =
        let values = enterValues []

        let phi = (1.0 + System.Math.Sqrt 5.0) / 2.0
        let pairs = values |> List.map (fun x -> (x, float x * phi) )

        pairs |> List.rev |> output

        let r = Console.ReadKey()
        0
