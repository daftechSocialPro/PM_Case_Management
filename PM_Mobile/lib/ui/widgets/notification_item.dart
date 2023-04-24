import 'package:flutter/material.dart';

class NotificationIcon extends StatelessWidget {
  int counter;
  NotificationIcon({required this.counter,Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return new Stack(
      children: <Widget>[
        new Icon(Icons.notifications),
        new Positioned(
          right: 0,
          child: new Container(
            padding: EdgeInsets.all(1),
            decoration: new BoxDecoration(
              color: Colors.red,
              borderRadius: BorderRadius.circular(6),
            ),
            constraints: BoxConstraints(
              minWidth: 12,
              minHeight: 12,
            ),
            child: new Text(
              counter != -1 ? '$counter' : '?',
              style: new TextStyle(
                color: Colors.white,
                fontSize: 8,
              ),
              textAlign: TextAlign.center,
            ),
          ),
        )
      ],
    );
  }
}
