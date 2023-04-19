// See https://aka.ms/new-console-template for more information
using langchain.LLMs;

Console.WriteLine("Hello, World!");

var openAi = new OpanAI();

var result = await openAi.Call(prompt:"hello, can you tell me what is your name?");