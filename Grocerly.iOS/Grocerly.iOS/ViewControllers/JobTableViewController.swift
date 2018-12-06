//
//  JobTableViewController.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class JobTableViewController: UIViewController, UITableViewDataSource {
    
    @IBOutlet weak var jobTable: UITableView!
    
    var shoppinglists = [ShoppingList]()

    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.jobTable.dataSource = self
        
        mockShoppingLists()
    }

    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return shoppinglists.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cellIdentifier = "JobCell"
        guard let cell = tableView.dequeueReusableCell(withIdentifier: cellIdentifier, for: indexPath) as? JobTableViewCell else {
            fatalError("Could not cast cell to jobcell")
        }
        
        let shoppinglist = shoppinglists[indexPath.row]
        
        cell.jobLabel.text = shoppinglist.name
        cell.imageView?.image = shoppinglist.image

        return cell
    }
    
    private func mockShoppingLists() {
        let shoppinglist1 = ShoppingList(name: "Ad minius' shoppinglist", id: "qwerty", image: UIImage(named: "mareike")!)
        let shoppinglist2 = ShoppingList(name: "Tom R's shoppinglist", id: "wertyu", image: UIImage(named: "tom")!)
        let shoppinglist3 = ShoppingList(name: "Sander Dl's shoppinglist", id: "rtyui", image: UIImage(named: "sander")!)
        
        shoppinglists += [shoppinglist1, shoppinglist2, shoppinglist3]
    }

}
