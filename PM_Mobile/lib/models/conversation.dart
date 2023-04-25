import 'package:daf_project1/models/conversation_replay.dart';

class Conversation{
 

  static final tblConversation =  'Conversation';
  static final colId = 'id';
  static final colConvid = 'c';
  static final colSenderId = 'senderId';
  static final colSenderName = 'sendeName';
  static final colSenderProfilePath= 'senderProfilePath';
  static final colMessage = 'message';
  static final colSentTime = 'sentTime';
  static final colTaskId = 'taskId';



Conversation.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _senderId= map[colSenderId];
  _senderName= map[colSenderName];
  _senderProfilePath= map[colSenderProfilePath];
  _message= map[colMessage];
  
  _taskId= map[colTaskId];
  _convId = map[colConvid];
  
  }

  late String _id;
  late String _senderId;
  late String _senderName;
  late String _senderProfilePath;
  late String _message;
  late List<Replay> _replies;
  late DateTime? _sentTime;
  late String _taskId ;
  late String _convId;


 String get convId =>_convId;
   set convId(String tid) {_convId=tid;}

   String get taskId =>_taskId;
   set taskId(String tid) {_taskId=tid;}

   String get senderId => _senderId;
   set senderId(String tid) {_senderId=tid;}
   
   DateTime? get sentTime => _sentTime;
   set sentTime(DateTime? tid) {_sentTime=tid;}
   
   String get id => _id;
  
   String get senderName => _senderName;
     set senderName(String tid) {_senderName=tid;}

   String get senderProfilePath => _senderProfilePath;
   set senderProfilePath(String tid) {_senderProfilePath=tid;}

   String get message => _message;
     set message(String tid) {_message=tid;}


 List<Replay> get replies => _replies;
set  replies(List<Replay> g) {
  _replies=g;
}

  Conversation(this._senderId, this._senderName, this._senderProfilePath,this._message, this._replies, this._sentTime);

  Conversation.fromJson(Map<String,dynamic> json):
        this._id = json["taskMemoId"],
        this._senderId = json['employeeId'],
        this._senderName = json['employeeName'],
        this._senderProfilePath = json['imagePath'],
        this._message = json['message'],
        this._sentTime = DateTime.parse(json['sentTime']),
       this._replies = json['taskMemoReplay'] != null ?  List<Replay>.from(json['taskMemoReplay'].map((e)=>Replay.fromJson(e))).toList() : []

   ;


  
}