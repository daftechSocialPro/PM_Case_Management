import 'package:flutter/material.dart';

import 'custom_subtitle.dart';

class DownloadItem extends StatelessWidget {
  final VoidCallback onPressed;
  final String fileName;
  final String label;
  final bool isFileExist;

  DownloadItem({required this.isFileExist, required this.label, required this.onPressed, required this.fileName, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Expanded(child: SubtitleText(this.label)),
        Expanded(
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
            IconButton(onPressed: this.onPressed, icon: Icon(isFileExist ? Icons.open_in_new_outlined : Icons.error_outline, color: Theme.of(context).primaryColor,)),
            Expanded(child: Text(fileName.substring(fileName.length > 20 ? fileName.length - 20 : 0), overflow: TextOverflow.ellipsis,))
          ],),
        ),
      ],
    );
  }
}
