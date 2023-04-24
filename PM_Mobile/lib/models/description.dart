class Description{
  late String _id;
  late String _description;

  String get id => _id;

  Description.fromJson(Map<String,dynamic> json):
    this._id = json[r"$id"],
    this._description = json["description"];

  String get description => _description;
}