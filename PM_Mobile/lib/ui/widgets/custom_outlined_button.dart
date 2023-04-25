
import 'package:daf_project1/viewmodels/ui_state.dart';
import 'package:flutter/material.dart';

class CustomOutlinedButton extends StatelessWidget {
  final VoidCallback onPressed;
  final String buttonText;
  final ButtonState buttonState;
  CustomOutlinedButton({required this.onPressed,required this.buttonText,this.buttonState = ButtonState.idle});

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    return  Container(
      padding: EdgeInsets.only(top: 3, left: 3),
      child: OutlinedButton(
        key:ValueKey(this.buttonText),
        onPressed: () {
          onPressed();
        },
        child: buttonState == ButtonState.loading
            ? CircularProgressIndicator(
          backgroundColor: Colors.white,
        )
            : Text(
          buttonText,
          style: theme.textTheme.button!.copyWith(
            color: theme.primaryColor
          ),
        ),
      ),
    );
  }
}