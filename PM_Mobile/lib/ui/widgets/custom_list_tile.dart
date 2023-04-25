

import 'package:flutter/material.dart';

class CustomListTile extends StatelessWidget {
  final Widget? leading;
  final Widget? trailing;
  final Widget title;
  CustomListTile({required this.title, this.leading, this.trailing, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return  ListTile(
      minLeadingWidth: 16,
      leading: leading,
      title: title,
      trailing: trailing
    );
  }
}
