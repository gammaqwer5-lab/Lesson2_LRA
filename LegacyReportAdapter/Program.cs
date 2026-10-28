using LegacyReportAdapter;
using ReportAdapter = LegacyReportAdapter.LegacyReportAdapter;

var legacyPrinter = new LegacyReportPrinter();
IReportExporter exporter = new ReportAdapter(legacyPrinter);
var document = new ReportDocument("Состояние оборудования",
    "Всего позиций: 18\nВ ремонте: 3\nСписано: 1");
exporter.Export(document);
