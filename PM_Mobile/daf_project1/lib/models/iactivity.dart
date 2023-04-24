
abstract class Activity{
  late String _id;
  late String _taskId;
  late String _description;
  late DateTime? _shouldStart;
  late DateTime? _shouldEnd;

  String get id => _id;
  late DateTime? _actuallyStarted;
  late DateTime? _actuallyEnded;
  late double _plannedBudget;
  late double _actualBudget;
  late double _weight;

  String get taskId => _taskId;
  late double _actualWorked;
  late double _goal;
  late String? _employeeId;
  late int _status;

  String get description => _description;

  DateTime? get shouldStart => _shouldStart;

  DateTime? get shouldEnd => _shouldEnd;

  DateTime? get actuallyStarted => _actuallyStarted;

  DateTime? get actuallyEnded => _actuallyEnded;

  double get plannedBudget => _plannedBudget;

  double get actualBudget => _actualBudget;

  double get weight => _weight;

  double get actualWorked => _actualWorked;

  double get goal => _goal;

  int get status => _status;
}