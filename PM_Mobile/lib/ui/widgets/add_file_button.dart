import 'package:daf_project1/services/file_picker.dart';
import 'package:flutter/material.dart';

import '../../locator.dart';

class AddFileButton extends StatelessWidget {
  VoidCallback onPressed;
  final String label;
  final bool showIcon;
  AddFileButton({this.showIcon = true, required this.onPressed, required this.label, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  OutlinedButton(
        onPressed: onPressed,
        child: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            this.showIcon ? Icon(Icons.attach_file) : Container(),
            Flexible(
                fit: FlexFit.loose,
                child: Text(label, overflow: TextOverflow.ellipsis,)),
          ],
        ));
  }
}
