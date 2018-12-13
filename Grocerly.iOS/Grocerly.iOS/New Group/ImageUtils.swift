
import UIKit

class ImageUtils {
    func imageFromUrl(url: String, targetImage: @escaping (UIImage) -> ()){
        let imageUrl = URL(string: url)!
        downloadImage(from: imageUrl, completion: targetImage)
    }
    
    private func downloadImage(from url: URL, completion: @escaping (UIImage) -> ()){
        print("Download Started")
        getData(from: url) { data, response, error in
            guard let data = data, error == nil else { return }
            print(response?.suggestedFilename ?? url.lastPathComponent)
            print("Download Finished")
            DispatchQueue.main.async() {
                completion(UIImage(data: data)!)
            }
        }
    }
    
    private func getData(from url: URL, completion: @escaping (Data?, URLResponse?, Error?) -> ()) {
        URLSession.shared.dataTask(with: url, completionHandler: completion).resume()
    }
    
}
