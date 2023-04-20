// See https://aka.ms/new-console-template for more information
using langchain.LLMs;

Console.WriteLine("Hello, World!");

var openAi = new OpanAI();

//var result = await openAi.Call(prompt:"hello, can you tell me what is your name?");

var prompt = new List<string> { "Tell me a joke", "Tell me a joke" , "what is your name?" };

foreach (var item in prompt)
{
    Console.WriteLine(item);
}

var result = await openAi.Generate(prompt);

foreach (var generation in result.Generations)
{
    foreach (var gen in generation)
    {
        Console.WriteLine(gen.Text);
    }
}