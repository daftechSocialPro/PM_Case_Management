
import 'package:flutter/material.dart';

class CustomInputField extends StatelessWidget {

  String label;
  bool obscureText;
  TextEditingController controller;
  String errorMsg;
  String hint;
  bool isMultiLine;
  bool isNumber;
  Key? key;
  CustomInputField({required this.label,this.hint = "", this.obscureText = false,required this.controller,required this.errorMsg, this.isMultiLine = false,this.key, this.isNumber = false});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(label, style: Theme.of(context).textTheme.subtitle1!,),
        SizedBox(height: 4,),
        TextField(
          key: ValueKey(this.label),
          controller: controller,
          obscureText: obscureText,
          minLines: isMultiLine ? 5 : 1,
          maxLines: isMultiLine ? null : 1,
          keyboardType: isNumber ? TextInputType.number: null,
          decoration: InputDecoration(
            hintText: this.hint,
            errorText: errorMsg =="" ? null : errorMsg,
            errorStyle: TextStyle(color: Colors.red),
            contentPadding: EdgeInsets.symmetric(vertical: 8, horizontal: 8),
            enabledBorder: OutlineInputBorder(
                borderSide: BorderSide(color: (Colors.grey[400])!)
            ),
            border: OutlineInputBorder(
                borderSide: BorderSide(color: (Colors.grey[400])!)
            ),
          ),
        ),
        SizedBox(height: 16,),

      ],
    );
  }
}
