class Replay{


static final tblConvReply = 'Reply';

static final colId = 'id';
static final colSenderId = 'senderId';
static final colSenderName = 'senderName';
static final colSenderProfilePath= 'senderProfilePath';
static final colMessage ='message';
static final colSentTime = 'sentTime';

static final colConversationId = 'conversationId' ;


Replay.fromMap (Map<dynamic,dynamic> map){

  _id= map[colId].toString();
  _senderId= map[colSenderId];
  _senderName= map[colSenderName];
  _senderProfilePath= map[colSenderProfilePath];
  _message= map[colMessage];
  _conversationId= map[colConversationId];
  
  }

  late String _id;
  late String _senderId;
  late String _senderName;
  late String _senderProfilePath;
  late String _message;
  late DateTime? _sentTime;
  late String _conversationId;
  
  String get conversationId => _conversationId;
  set conversationId(String t){
    _conversationId=t;
  }
  String get senderId => _senderId;
  String get id => _id;
  DateTime? get sentTime => _sentTime;
  String get senderName => _senderName;
  String get senderProfilePath => _senderProfilePath;
  String get message => _message;

  Replay(this._senderId, this._senderName, this._senderProfilePath,this._message, this._sentTime);

  Replay.fromJson(Map<String,dynamic> json):
        this._id = json["taskMemoReplayId"],
        this._senderId = json['employeeId'],
        this._senderName = json['employeeName'],
        this._senderProfilePath = json['imagePath'],
        this._message = json['message'] ?? "",
        this._sentTime = DateTime.parse(json['sentTime']);


}