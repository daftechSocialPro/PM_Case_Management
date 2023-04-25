import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';

class PrimaryButton extends StatelessWidget {
  final VoidCallback? onPressed;
  final String buttonText;
  final ButtonState buttonState;
  PrimaryButton({required this.onPressed,required this.buttonText,required this.buttonState});

  @override
  Widget build(BuildContext context) {
    var theme  = Theme.of(context);
    return  Container(
      padding: EdgeInsets.only(top: 3, left: 3),
      child: ElevatedButton(
        key:ValueKey(this.buttonText),
        onPressed: onPressed,

        //shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(50)),
        child: buttonState == ButtonState.loading
            ? CircularProgressIndicator(
          backgroundColor: Colors.white,
        )
            : Text(
          buttonText,
          style: theme.textTheme.button!.copyWith(
            color: Colors.white
    ),
      ),
    ));
  }
}