import 'dart:io';

import 'package:file_picker/file_picker.dart';

class FilePickerUtil{

  Future<File?> getFile() async{
    FilePickerResult? result = await FilePicker.platform.pickFiles();
    if(result != null) {
      String? path = result.files.single.path;
      if(path != null){
        File file = File(path);
        return file;
      }
    } else {
      // User canceled the picker
    }
  }

  Future<List<File?>?> getFiles() async{
    FilePickerResult? result = await FilePicker.platform.pickFiles(allowMultiple: true);
    if(result != null) {
      List<File?> files = result.paths.map((path){
        if(path != null){
          return File(path);
        }
      }).toList();
      return files;
    } else {
      // User canceled the picker
    }

  }
}
