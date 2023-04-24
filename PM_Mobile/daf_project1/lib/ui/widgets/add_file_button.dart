import 'package:daf_project1/services/file_picker.dart';
import 'package:flutter/material.dart';

import '../../locator.dart';

class AddFileButton extends StatelessWidget {
  FilePickerUtil filePickerUtil = locator<
      FilePickerUtil>();

  String label;
  bool selectMultipleFiles;
  AddFileButton({required this.label, required this.selectMultipleFiles, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  OutlinedButton(
        onPressed: () async{
          selectMultipleFiles ? filePickerUtil.getFiles() : filePickerUtil.getFile();
        },
        child: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(Icons.attach_file),
            Text(label),
          ],
        ));
  }
}
