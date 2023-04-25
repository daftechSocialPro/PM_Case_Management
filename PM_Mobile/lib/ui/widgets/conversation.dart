import 'package:daf_project1/models/conversation.dart' as model;
import 'package:daf_project1/ui/widgets/chat_composer.dart';
import 'package:daf_project1/utils/date_formatter.dart';
import 'package:daf_project1/viewmodels/task_detail_viewmodel.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class Conversation extends StatelessWidget {
  final model.Conversation conversation;
  final int conversationIndex;
  final bool isReplay;

  Conversation(
      {this.conversationIndex = -1,
      required this.conversation,
      this.isReplay = false,
      Key? key})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    TaskDetailViewModel taskDetailViewModel =
        Provider.of<TaskDetailViewModel>(context, listen: false);
    bool isMe = taskDetailViewModel.userId == conversation.senderId;
    int repliesLength = conversation.replies.length;
    var theme = Theme.of(context);
    return Container(
      margin: EdgeInsets.only(top: 10),
      child: Column(
        children: [
          Row(
            mainAxisAlignment:
                isMe ? MainAxisAlignment.end : MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.end,
            children: [
              if (!isMe)
                Tooltip(
                  message: conversation.senderName,
                  child: CircleAvatar(
                    radius: 15,
                    backgroundImage: NetworkImage(
                        taskDetailViewModel.relativeImagePath +
                            conversation.senderProfilePath),
                  ),
                ),
              SizedBox(
                width: 10,
              ),
              Container(
                padding: EdgeInsets.all(10),
                constraints: BoxConstraints(
                    maxWidth: MediaQuery.of(context).size.width * 0.6),
                decoration: BoxDecoration(
                    color:
                        isMe ? Theme.of(context).accentColor : Colors.grey[200],
                    borderRadius: BorderRadius.only(
                      topLeft: Radius.circular(16),
                      topRight: Radius.circular(16),
                      bottomLeft: Radius.circular(isMe ? 12 : 0),
                      bottomRight: Radius.circular(isMe ? 0 : 12),
                    )),
                child: Text(
                  conversation.message,
                  style: Theme.of(context)
                      .textTheme
                      .bodyText1!
                      .copyWith(color: isMe ? Colors.white : Colors.grey[800]),
                ),
              ),
            ],
          ),
          isReplay
              ? Container()
              : Padding(
                  padding: const EdgeInsets.only(top: 5),
                  child: Row(
                    mainAxisAlignment:
                        isMe ? MainAxisAlignment.end : MainAxisAlignment.start,
                    children: [
                      if (!isMe)
                        SizedBox(
                          width: 40,
                        ),
                      !isMe
                          ? Container()
                          : TextButton(
                              onPressed: () => {
                                _onPressed(
                                    context,
                                    taskDetailViewModel.replayTextController,
                                    () => {
                                          taskDetailViewModel
                                              .addReplay(conversationIndex)
                                        })
                              },
                              child: Text("$repliesLength Replies"),
                              style: TextButton.styleFrom(
                                tapTargetSize: MaterialTapTargetSize.shrinkWrap,
                                padding: EdgeInsets.symmetric(horizontal: 16),
                              ),
                            ),
                      Text(
                        conversation.sentTime != null
                            ? "${DateFormatter.getVerboseDateTimeRepresentation(conversation.sentTime ?? DateTime.now())}"
                            : "Unknown",
                        // style: MyTheme.bodyTextTime,
                      ),
                      isMe
                          ? Container()
                          : TextButton(
                              onPressed: () => {
                                _onPressed(
                                    context,
                                    taskDetailViewModel.replayTextController,
                                    () => {
                                          taskDetailViewModel
                                              .addReplay(conversationIndex)
                                        })
                              },
                              child: Text("$repliesLength Replies"),
                              style: TextButton.styleFrom(
                                tapTargetSize: MaterialTapTargetSize.shrinkWrap,
                                padding: EdgeInsets.symmetric(horizontal: 16),
                              ),
                            ),
                    ],
                  ),
                )
        ],
      ),
    );
  }

  _onPressed(BuildContext context, TextEditingController controller,
      VoidCallback onPressed) {
    showModalBottomSheet(
        isDismissible: true,
        context: context,
        isScrollControlled: true,
        backgroundColor: Colors.transparent,
        builder: (context) {
          return Consumer<TaskDetailViewModel>(builder: (context, value, _) {
            return Container(
              color: Colors.white,
              margin: EdgeInsets.only(top: 128),
              padding: EdgeInsets.only(left: 16, right: 16, bottom: MediaQuery.of(context).viewInsets.bottom),
              child: DraggableScrollableSheet(
                  expand: false,
                  builder: (BuildContext context,
                      ScrollController scrollController) {
                    return Column(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        ChatComposer(
                            textEditingController: controller,
                            onPressed: onPressed,
                            isReplay: true),
                        Expanded(
                          child: SingleChildScrollView(
                            controller: scrollController,
                            child: Column(
                                mainAxisSize: MainAxisSize.min,
                                children: List<Conversation>.from(conversation
                                    .replies
                                    .map((e) => Conversation(
                                          conversation: model.Conversation(e.senderId,e.senderName,e.senderProfilePath,e.message,[],e.sentTime),
                                          isReplay: true,
                                        ))).toList()),
                          ),
                        ),
                      ],
                    );
                  }),
            );
          });
        },
        shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
                topLeft: Radius.circular(10.0),
                topRight: Radius.circular(10.0))));
  }
}
