using LegacyReportAdapter;
using ReportAdapter = LegacyReportAdapter.LegacyReportAdapter;
var failed = new List<string>();
void Check(string name, bool condition)
{
    Console.WriteLine($"{(condition ? "PASS" : "FAIL")} — {name}");
    if (!condition) failed.Add(name);
}
var output = new StringWriter();
var original = Console.Out;
try
{
    Console.SetOut(output);
    IReportExporter exporter = new ReportAdapter(new LegacyReportPrinter());
    exporter.Export(new ReportDocument("Test title", "Test body"));
}
finally { Console.SetOut(original); }
var text = output.ToString();
Check("заголовок передан legacy-принтеру", text.Contains("=== Test title ==="));
Check("тело передано legacy-принтеру", text.Contains("Test body"));
Check("сохранён завершающий маркер", text.Contains("=== END OF REPORT ==="));

// TODO 5: Добавьте тест пустого заголовка или тела согласно контракту.
var emptyTitleRejected = false;
try
{
    IReportExporter emptyTitleExporter = new ReportAdapter(new LegacyReportPrinter());
    emptyTitleExporter.Export(new ReportDocument("", "тело"));
}
catch (ArgumentException) { emptyTitleRejected = true; }
Check("пустой заголовок", emptyTitleRejected);

// TODO 6: Добавьте тест null-документа (можно использовать null!).
var nullDocumentRejected = false;
try
{
    IReportExporter nullDocumentExporter = new ReportAdapter(new LegacyReportPrinter());
    nullDocumentExporter.Export(null!);
}
catch (ArgumentNullException) { nullDocumentRejected = true; }
Check("null-документ", nullDocumentRejected);

// TODO 7: Добавьте тест, что конструктор не принимает null-зависимость.
var nullPrinterRejected = false;
try
{
    new ReportAdapter(null!);
}
catch (ArgumentNullException) { nullPrinterRejected = true; }
Check("конструктор не принимает null-зависимость", nullPrinterRejected);

Console.WriteLine($"Провалено базовых проверок: {failed.Count} из 3.");
