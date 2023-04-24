import 'package:daf_project1/models/description.dart';

class Notification{
  late String _id;
  late List<Description> _description;
  late String _title;

  String get id => _id;

  Notification();

  Notification.fromJson(Map<String,dynamic> json):
    this._id = json[r'$id'],
    this._description = List<Description>.from((json['description']).map((e) => Description.fromJson(e)).toList()),
    this._title = json['planname'];



  List<Description> get description => _description;

  String get title => _title;
}