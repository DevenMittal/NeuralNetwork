import AppKit
let args = CommandLine.arguments
let rows = try String(contentsOfFile: args[1], encoding: .utf8).split(separator: "\n").dropFirst()
try FileManager.default.createDirectory(atPath: args[2], withIntermediateDirectories: true)
for (index,row) in rows.enumerated() {
 let fields = row.split(separator: ",").map(String.init)
 let image = NSImage(size: NSSize(width: 720, height: 400))
 image.lockFocus()
 NSColor(calibratedWhite: 0.98, alpha: 1).setFill(); NSRect(x:0,y:0,width:720,height:400).fill()
 func text(_ s: String,_ x: CGFloat,_ y: CGFloat,_ size: CGFloat = 16) {
  (s as NSString).draw(at:NSPoint(x:x,y:y),withAttributes:[.font:NSFont.monospacedSystemFont(ofSize:size,weight:.regular),.foregroundColor:NSColor.black])
 }
 text("XOR learning | original C# network",30,355,22)
 text("2-2-1 tanh / seed 42 / learning rate 0.01",30,324,15)
 text("Training steps: \(fields[0])",30,287)
 let labels = ["0,0 -> 0","1,0 -> 1","0,1 -> 1","1,1 -> 0"]
 for i in 0..<4 {
  let x = CGFloat(65+i*165), value = Double(fields[i+1])!
  NSColor(calibratedWhite:0.88,alpha:1).setFill();NSRect(x:x,y:92,width:100,height:150).fill()
  NSColor(calibratedRed:0.19,green:0.42,blue:0.30,alpha:1).setFill();NSRect(x:x,y:92,width:100,height:150*max(0,min(1,value))).fill()
  text(String(format:"%.4f",value),x+6,250,16)
  text(labels[i],x-5,63,15)
 }
 text(String(format:"Sum squared error: %.7f",Double(fields[5])!),30,26,15)
 image.unlockFocus()
 let bitmap = NSBitmapImageRep(data:image.tiffRepresentation!)!
 try bitmap.representation(using:.png,properties:[:])!.write(to:URL(fileURLWithPath:args[2]+String(format:"/frame-%03d.png",index)))
}
