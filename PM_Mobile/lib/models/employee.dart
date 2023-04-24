class Employee{
  late String _id;
  late String _fullName;
  late String _amharicFullName;

  String get id => _id;

  late String _imagePath;

  Employee({id, fullName, amharicFullName, imagePath}){
      this._id = id;
      this._fullName = fullName;
      this._amharicFullName = amharicFullName;
      this._imagePath = imagePath;

  }


  @override
  String toString() {
    return _fullName;
  }

  Employee.fromJson(Map<String,dynamic> json):
    _id = json['id'],
    _fullName = json['employeeFullName'],
    _amharicFullName = json['employeeAmharicFullName'],
    _imagePath = json['image'];

  String get fullName => _fullName;

  String get amharicFullName => _amharicFullName;

  String get imagePath => _imagePath;


}