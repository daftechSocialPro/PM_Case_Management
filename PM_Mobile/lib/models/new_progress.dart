class Progress{

  // request.fields['ActivityId'] = ;
// request.fields['ActualBudget'] = ;
// request.fields['ActualWorked'] = ;
// request.fields['EmployeeValueId'] = ;
// request.fields['Remark'] = ;
// request.fields['lat'] = ;
// request.fields['lng'] = ;
late String _activityId;
late String _employeeId;
late String _remark;
late double _lat;
late double _lng;
late double _actualWorked;
late double _actualBudget;
late int _pros;
late String _quarterId;


int get pros => _pros;

  Progress(this._activityId, this._employeeId, this._remark, this._lat,
      this._lng, this._actualWorked, this._actualBudget, this._pros, this._quarterId);

  String get activityId => _activityId;

String get employeeId => _employeeId;

double get actualBudget => _actualBudget;

  double get actualWorked => _actualWorked;

  double get lng => _lng;

  double get lat => _lat;

  String get remark => _remark;

String get quarterId => _quarterId;
}