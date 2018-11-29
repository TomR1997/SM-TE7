//
//  JobTableViewController.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class JobTableViewController: UITableViewController {
    
    var shoppinglists = [ShoppingList]()

    override func viewDidLoad() {
        super.viewDidLoad()
        mockShoppingLists()
    }

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return shoppinglists.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cellIdentifier = "JobCell"
        guard let cell = tableView.dequeueReusableCell(withIdentifier: cellIdentifier, for: indexPath) as? JobTableViewCell else {
            fatalError("Could not cast cell to jobcell")
        }
        
        let shoppinglist = shoppinglists[indexPath.row]
        
        cell.jobLabel.text = shoppinglist.name

        return cell
    }
    
    private func mockShoppingLists() {
        let shoppinglist1 = ShoppingList(name: "Ad minius' shoppinglist", id: "qwerty")
        let shoppinglist2 = ShoppingList(name: "Tom R's shoppinglist", id: "wertyu")
        let shoppinglist3 = ShoppingList(name: "Sander Dl's shoppinglist", id: "rtyui")
        
        shoppinglists += [shoppinglist1, shoppinglist2, shoppinglist3]
    }

}
