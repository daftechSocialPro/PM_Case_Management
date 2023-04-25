import 'package:flutter/material.dart';
import 'package:photo_view/photo_view.dart';

class FullScreenPage extends StatelessWidget {
  String path;
  FullScreenPage({required this.path});
  @override
  Widget build(BuildContext context) {
    return Container(
      child: PhotoView(
        loadingBuilder: (context, event) => Center(



          child: Container(
            width: 20.0,
            height: 20.0,
            child: CircularProgressIndicator(
              value: event == null
                  ? 0
                  : event.cumulativeBytesLoaded / (event.expectedTotalBytes ?? 1),
            ),
          ),
        ),
        imageProvider: NetworkImage("$path"),
      )
    );
  }
}