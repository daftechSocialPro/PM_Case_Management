import 'package:flutter/material.dart';

class ChatComposer extends StatelessWidget {

  final bool isReplay;
  final VoidCallback onPressed;
  final TextEditingController textEditingController;
  ChatComposer({required this.textEditingController, required this.onPressed,this.isReplay = false, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.white,
      height: 100,
      child: Row(
        children: [
          Expanded(
            child: Container(
              padding: EdgeInsets.symmetric(horizontal: 8),
              height: 60,
              decoration: BoxDecoration(
                color: Colors.grey[200],
                borderRadius: BorderRadius.circular(30),
              ),
              child: Row(
                children: [
                  IconButton(
                    onPressed: ()=>{

                    },
                    icon: Icon(Icons.emoji_emotions_outlined),
                    color: Colors.grey[500],
                  ),
                  SizedBox(
                    width: 10,
                  ),
                  Expanded(
                    child: TextField(
                      controller: textEditingController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: isReplay ? "Add replay ..." : 'Add comment ...',
                        hintStyle: TextStyle(color: Colors.grey[500]),
                      ),
                    ),
                  ),
                  GestureDetector(
                    onTap: onPressed,
                    child: CircleAvatar(
                      //backgroundColor: MyTheme.kAccentColor,
                      child: Icon(
                        Icons.send_outlined,
                        color: Colors.white,
                      ),
                    ),
                  )
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
