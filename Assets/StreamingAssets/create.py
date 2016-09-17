import xlrd
import sqlite3,os,time,json

class sql(object):
	def __init__(self):
		super(sql, self).__init__()
		db = 'database.db'
		if os.path.exists(db):
			os.remove(db)

		self.conn = sqlite3.connect(db)

		print "Open",db,"Success"
		
	def __del__(self):
		self.conn.close()

	def create_table(self, table):
		nrows = table.nrows
		ncols = table.ncols
		s = """CREATE TABLE """ + table.name + """(\n\tid INTEGER PRIMARY KEY"""
		for j in xrange(1,ncols):
			cell = table.cell(0,j).value 
			
			s = s + ",\n\t" + cell.encode("utf-8") + " TEXT NOT NULL"
			# print cell
		s = s + "\n);"
		# print s
		self.conn.execute(s)

	def insert_monster(self, table, i):
		nrows = table.nrows
		ncols = table.ncols
		s = "INSERT INTO " + table.name + "(id"
		for j in xrange(1,ncols):
			cell = table.cell(0,j).value 
			if type(cell) is float:
				s = s + "," + str(cell)
			elif type(cell) is unicode:
				s = s + "," + cell.encode("utf-8")
		s = s + ") VALUES(" + str(table.cell(i,0).value) 
		for j in xrange(1,ncols):
			cell = table.cell(i,j).value 

			if type(cell) is float:
				if float(int(cell)) == cell:
					s = s + ",'" + str(int(cell)) + "'"
				else:
					s = s + ",'" + str(cell) + "'"
			elif type(cell) is unicode:
				s = s + ",'" + cell + "'"
			# print cell
		
		s = s + ");"
		print s.encode("utf-8")
		self.conn.execute(s)
		self.conn.commit()

	def execute(self, seq):
		return self.conn.execute(seq)

s = sql();

data = xlrd.open_workbook("monsters.xlsx")
tables =  data.sheets()
for table in tables:
	if table.name == "setting":
		continue
	print table.name
	s.create_table(table)
	nrows = table.nrows

	for i in xrange(1,nrows):
		s.insert_monster(table, i)