//
//  JobTableViewController.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class ItemViewController: UIViewController, UITableViewDataSource {
    
    @IBOutlet weak var itemTable: UITableView!
    
    var items = [Item]()
    var shoppinglistItems = [Item]()
    var client = HttpClient()
    var imageUtils = ImageUtils()
    var baseString = "i315103core.venus.fhict.nl"
    var selectedShoppinglist: ShoppingList?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.itemTable.dataSource = self
        
        getProducts()
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return shoppinglistItems.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cellIdentifier = "ItemCell"
        guard let cell = tableView.dequeueReusableCell(withIdentifier: cellIdentifier, for: indexPath) as? ItemTableCell else {
            fatalError("Could not cast cell to itemcell")
        }
        
        let item = shoppinglistItems[indexPath.row]
        imageUtils.imageFromUrl(url: item.imageUrl) {targetImage in
            cell.itemView.image = targetImage
        }
        
        cell.ItemLabel.text = item.name
        
        return cell
    }
    
    private func getProducts(){
        var components = URLComponents()
        components.scheme = "http"
        components.host = baseString
        components.path = "/api/products"
        
        let url = components.url
        
        client.get(url: url!, ofType: [Item].self) { (item, response, error) in
            self.items = item ?? [Item]()
            
            if (self.selectedShoppinglist != nil){
                for _ in 0..<self.selectedShoppinglist!.shoppinglistItems {
                    self.shoppinglistItems.append(self.items[Int.random(in: 0..<(self.selectedShoppinglist?.shoppinglistItems)!)])
                }
            }
            
            self.itemTable.reloadData()
        }
    }
    @IBAction func backButtonClick(_ sender: Any) {
        self.navigationController?.popViewController(animated: true)
    }
    
}
