import 'employee.dart';

class ActivityProgress{
  late String _id;
  late double _actualBudget;
  late double _actualWorked;
  late String? _documentPath;
  late bool _isApprovedByCoordinator;
  late bool _isApprovedByFinance;
  late String _employeeId;
  late Employee _employee;
  late String _remark;

  String get id => _id;
  ActivityProgress();

  ActivityProgress.fromJson(Map<String,dynamic> json):
    _id = json['id'],
    _actualBudget = double.parse((json['actualBudget']??0).toString()),
    _actualWorked = double.parse((json['actualWorked']??0).toString()),
    _employeeId = json['employeeValueId'],
    _employee = Employee.fromJson(json['employeeValue']),
    _documentPath= json['documentPath'],
    _isApprovedByCoordinator = json['isApprovedByCoordinator'],
    _isApprovedByFinance = json['isApprovedByFinance'];


  @override
  String toString() {
    return _id;
  }

  double get actualBudget => _actualBudget;

  String get remark => _remark;

  Employee get employee => _employee;

  String get employeeId => _employeeId;

  bool get isApprovedByFinance => _isApprovedByFinance;

  bool get isApprovedByCoordinator => _isApprovedByCoordinator;

  String? get documentPath => _documentPath;

  double get actualWorked => _actualWorked;
}