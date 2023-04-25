class Employee{

static final tblEmployee = 'Employee';
static final colId = 'id';
static final colUserId='userId';
static final colFullName ='fullName';
static final colAmharicFullName ='amharicFullName';
static final colUsernName = 'username';
static final colPassword = 'password';

static final colMembershipLevel='memberShipLevel';
static final colStructure= 'structure';
static final colImagePath = 'imagePath';



Employee.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _fullName= map[colFullName];
  _amharicFullName= map[colAmharicFullName];
 _userId=map[colUserId];
  _username= map[colUsernName];
  _password= map[colPassword];
  _memberShipLevel= map[colMembershipLevel];
  _structure=map[colStructure];
  _imagePath =map[colImagePath];
  
}

Map <String ,dynamic> toMap () {


  var map= <String ,dynamic> {colFullName:_fullName,colAmharicFullName:_amharicFullName
  ,colUsernName:_username,colPassword:_password,colMembershipLevel:_memberShipLevel,
  colStructure:_structure,colImagePath:_imagePath
  };

  if (_id!=null)
  map[colId]=_id;

  return map;
}



  late String _id;
  late String _fullName;
  late String _amharicFullName;
  String? _username;
  String? _password;
  String ?_userId;
  String? _memberShipLevel;
  String? _structure;

String ?  get userId => _userId;
set userId (String ? id){
  _userId=id;
}

set id (String Userid){
  _id= Userid;
}

  String? get username => _username;
set username (String? name) {
    _username = name;
  }
String? get password => _password;
set password (String? password) {
    _password = password;
  }

  String get id => _id;

  late String _imagePath;


  @override
  String toString() {
    return _fullName;
  }

  Employee.fromJson(Map<String,dynamic> json):
    _id = json['id'],
    _fullName = json['employeeFullName'],
    _amharicFullName = json['employeeAmharicFullName'],
    _imagePath = json['image'],
    _username = json['username'],
    _memberShipLevel = json['memberShipLevel'],
    _structure = json['structure'];

  String get fullName => _fullName;

  String get amharicFullName => _amharicFullName;

  String get imagePath => _imagePath;

  String? get memberShipLevel => _memberShipLevel;

  String? get structure => _structure;
}