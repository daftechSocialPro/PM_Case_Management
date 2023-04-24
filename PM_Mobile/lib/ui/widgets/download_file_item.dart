import 'package:flutter/material.dart';

import 'custom_subtitle.dart';

class DownloadItem extends StatelessWidget {
  VoidCallback onPressed;
  String fileName;
  String label;
  bool isFileExist;

  DownloadItem({required this.isFileExist, required this.label, required this.onPressed, required this.fileName, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        SubtitleText(this.label),
        Row(
          mainAxisSize: MainAxisSize.min,
          children: [
          IconButton(onPressed: this.onPressed, icon: Icon(isFileExist ? Icons.download : Icons.error_outline, color: Theme.of(context).primaryColor,)),
          Text(fileName.substring(fileName.length > 25 ? fileName.length - 25: 0),)
        ],),
      ],
    );
  }
}
