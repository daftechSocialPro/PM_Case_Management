import 'package:flutter/material.dart';

class CustomCircularAvatar extends StatelessWidget {
  CustomCircularAvatar({required this.imagePath, required this.isSmall, Key? key}) : super(key: key);
  final String imagePath;
  final bool isSmall;

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: isSmall ? 20 : 37 ,
      backgroundColor: Colors.white,
      child: CircleAvatar(
        radius: isSmall ? 18 : 35,
        backgroundImage: NetworkImage(imagePath), //
        backgroundColor: Colors.black26,// Provide your custom image),
    )
    );
  }
}
